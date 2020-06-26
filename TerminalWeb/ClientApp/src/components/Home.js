import React, { useState, useEffect } from 'react';
import HubGenerator from '../services/hubGenerator';
import MachineRepository from '../repositories/MachineRepository';
import LogRepository from '../repositories/LogRepository';

export default () => {
  const [machines, setMachines] = useState([]);
  const [machine, setMachine] = useState(undefined);
  const [machineHub, setMachineHub] = useState(undefined);
  const [logs, setLogs] = useState([]);
  const [logHub, setLogHub] = useState(undefined);

  useEffect(() => {
    MachineRepository.getAll().then(({ data }) => {
      setMachines(data);
    });

    setMachineHub(HubGenerator.create('machineHub'));
    setLogHub(HubGenerator.create('logHub'));
  }, []);

  useEffect(() => {
    if (machineHub)
      machineHub.start().then(() => {
        machineHub.on("NewMachine", newMachine => {
          setMachines([...machines, newMachine]);
        });
      });
  }, [machineHub]);

  useEffect(() => {
    if (logHub)
      logHub.start().then(() => {
        logHub.on("ResponseLog", newLog => {
          if (newLog.machineId === machine.id)
            setLogs([...logs, newLog]);
        });
      });
  }, [logHub]);

  useEffect(() => {
    if (machine)
      LogRepository.getAllByMachineId(machine.id).then(({ data }) => {
        setLogs(data);
      });
  }, [machine]);

  return (
    <div>
      Hello, world!
    </div>
  );
}
