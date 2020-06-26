import React, { useState, useEffect } from 'react';
import MachineRepository from '../repositories/MachineRepository';

export default () => {
  const [machines, setMachines] = useState([]);
  const [logs, setLogs] = useState([]);

  useEffect(() => {
    MachineRepository.getAll().then(console.info)
  }, [])

  return (
    <div>
      Hello, world!
    </div>
  );
}
