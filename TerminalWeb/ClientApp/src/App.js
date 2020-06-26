import React, { Component } from 'react';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import Home from './components/Home';

const palette = { type: 'dark', primary: { main: '#90caf9' }, secondary: { main: '#f48fb1' }, error: { main: '#f44336' }, success: { main: '#c5e1a5' }, warning: { main: '#ffe082' } };
const theme = createMuiTheme({ palette });

export default class App extends Component {
  render() {
    return (
      <MuiThemeProvider theme={theme}>
        <Home />
      </MuiThemeProvider>
    );
  }
}
