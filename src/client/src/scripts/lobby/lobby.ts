import Guid from '../data/guid';
import Player from '../player';

export default class Lobby {
    public id: Guid;
    public players: Player[];
}