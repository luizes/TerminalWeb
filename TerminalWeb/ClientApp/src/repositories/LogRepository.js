import api from '../services/api';

const prefix = 'log';

export default class LogRepository {
    static getAllByMachineId(machineId) {
        return api.get(`${prefix}/${machineId}`);
    }

    static create(command, machineId) {
        return api.post(prefix, { command, machineId });
    }
}
