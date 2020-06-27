import React, { useState, useEffect } from "react";
import {
  LinearProgress,
  CssBaseline,
  AppBar,
  Toolbar,
  Typography,
  Container,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Grid,
  makeStyles,
  Snackbar,
  Chip,
  TableContainer,
  Table,
  TableHead,
  TableBody,
  TableRow,
  TableCell,
  Paper,
  Card,
  TextField,
  CardContent,
  List,
  ListItem,
  ListItemText
} from "@material-ui/core";
import { Alert } from "reactstrap";
import HubGenerator from "../services/hubGenerator";
import MachineRepository from "../repositories/MachineRepository";
import LogRepository from "../repositories/LogRepository";
import { parseSizeForGigaByte, parseDate } from "../converters";

const useStyles = makeStyles(theme => ({
  formControl: {
    padding: theme.spacing(1),
    minHeight: "100%",
    maxHeight: "100%",
  },
  chip: {
    margin: theme.spacing(1, 0.5, 1, 0)
  },
  label: {
    margin: theme.spacing(1, 0, 0, 1)
  },
  terminal: {
    height: "100%",
    borderRadius: 0,
    overflow: "auto",
    maxHeight: 375
  }
}));

export default () => {
  const classes = useStyles();
  const [loading, setloading] = useState(true);
  const [notification, setNotification] = useState("");
  const [machines, setMachines] = useState([]);
  const [machine, setMachine] = useState(undefined);
  const [machineHub, setMachineHub] = useState(undefined);
  const [logs, setLogs] = useState([]);
  const [logHub, setLogHub] = useState(undefined);
  const [newCommand, setNewCommand] = useState("");

  useEffect(() => {
    MachineRepository.getAll().then(({ data }) => {
      setMachines(data);
      setloading(false);
    });

    setMachineHub(HubGenerator.create("machineHub"));
    setLogHub(HubGenerator.create("logHub"));
  }, []);

  useEffect(() => {
    if (machineHub)
      machineHub.start().then(() => {
        machineHub.on("NewMachine", ({ success, data: newMachine }) => {
          if (success) {
            setMachines(others => [...others, newMachine]);
            setNotification(`${newMachine.name} conectada!`)
          }
        });
      });
  }, [machineHub]);

  useEffect(() => {
    if (logHub)
      logHub.start().then(() => {
        logHub.on("ResponseLog", ({ success, data: newLog }) => {
          if (success) {
            setLogs(others => [...others, newLog]);
            scrollToButton();
          }
        });
      });
  }, [logHub]);

  useEffect(() => {
    if (machine)
      LogRepository.getAllByMachineId(machine.id).then(({ data }) => {
        setLogs(data);
        scrollToButton();
      });
  }, [machine]);

  const createCommand = e => {
    let commandTrim = newCommand.trim();

    if (e.keyCode === 13 && commandTrim)
      logHub.invoke("Send", { machineId: machine.id, command: commandTrim }).then(() => {
        setNewCommand("");
        scrollToButton();
      });
  }

  const closeNotification = (event, reason) => {
    if (reason === "clickaway")
      return;

    setNotification("");
  }

  const scrollToButton = () => {
    let element = document.querySelector("#terminal");
    element.scrollTop = element.scrollHeight;
  }

  return (
    <React.Fragment>
      <CssBaseline />
      <AppBar>
        <Toolbar>
          <Typography variant="h6">TerminalWeb</Typography>
        </Toolbar>
      </AppBar>
      <Toolbar />
      {loading ? <LinearProgress color="secondary" /> : <Container fixed maxWidth={false}>
        <Grid container spacing={2}>
          <Grid item xs={12} sm={4}>
            <div className={classes.formControl}>
              <FormControl fullWidth>
                <InputLabel className={classes.label} margin="dense" id="select-machine-label">Máquina:</InputLabel>
                <Select variant="filled" labelId="select-machine-label" id="machine-select" onChange={e => setMachine(e.target.value)}>
                  {machines.map(m => <MenuItem key={m.id} value={m}>{m.name}</MenuItem>)}
                </Select>
              </FormControl>
              {machine && <React.Fragment>
                <br />
                <br />
                <Typography gutterBottom variant="h4">{machine.windowsVersion}</Typography>
                <Typography gutterBottom variant="h6">{machine.ipLocal}</Typography>
                <Typography color="textSecondary" variant="body2">Client instalado no dia {parseDate(machine.createdAt)}</Typography>
                <div>
                  <Chip className={classes.chip} label={`Antivirus ${machine.antivirusInstalled ? "Instalado" : "não Instalado"}`} />
                  <Chip className={classes.chip} label={`Firewall ${machine.firewallIsActive ? "Ativo" : "Desativado"}`} />
                </div>
                <Typography>Discos Rigídos:</Typography>
                <TableContainer component={Paper}>
                  <Table size="small" className={classes.table}>
                    <TableHead>
                      <TableRow>
                        <TableCell>Nome</TableCell>
                        <TableCell align="right">Tamanho</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {machine.diskDrives.map(disk => <TableRow key={disk.id}>
                        <TableCell>{disk.name}</TableCell>
                        <TableCell align="right">{parseSizeForGigaByte(disk.totalSize)}</TableCell>
                      </TableRow>)}
                    </TableBody>
                  </Table>
                </TableContainer>
              </React.Fragment>}
            </div>
          </Grid>
          {machine && <Grid item xs={12} sm={8}>
            <Card id="terminal" className={classes.terminal}>
              <CardContent>
                <List dense>
                  {logs.map(log => <ListItem key={log.id}>
                    <ListItemText key={log.id} primary={`> ${log.command}`} secondary={log.response} />
                  </ListItem>)}
                </List>
              </CardContent>
            </Card>
            <FormControl fullWidth>
              <TextField autoFocus id="cmd" label=">" variant="filled" value={newCommand} onChange={e => setNewCommand(e.target.value)} onKeyDown={createCommand} />
            </FormControl>
          </Grid>}
        </Grid>
      </Container>}
      <Snackbar open={Boolean(notification)} autoHideDuration={6000} onClose={closeNotification}>
        <Alert onClose={closeNotification} severity="success">{notification}</Alert>
      </Snackbar>
    </React.Fragment>
  );
}
