import numeral from 'numeral';
import { format, parseISO } from 'date-fns';
import locale from 'date-fns/locale/pt';

export function parseSizeForGigaByte(size) {
    return numeral(size).format('0.00b');
}

export function parseDate(date) {
    return format(parseISO(date), "dd 'de' MMMM' às ' HH:mm'h'", { locale })
}