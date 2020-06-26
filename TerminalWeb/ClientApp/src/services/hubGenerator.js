import { HubConnectionBuilder } from '@aspnet/signalr';

export default class HubGenerator {
    static create(prefix) {
        return new HubConnectionBuilder().withUrl(`${window.location.href}${prefix}`).build();
    }
}
