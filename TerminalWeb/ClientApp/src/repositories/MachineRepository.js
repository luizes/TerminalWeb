import api from '../services/api';

const prefix = 'machine';

export default class MachineRepository {
    static getAll() {
        return api.get(prefix);
    }
}
