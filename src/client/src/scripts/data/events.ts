import Guid from './guid';
export module Scripts.Data.Events.Game {
    'use strict';
    export class GameInfo {
        id: Guid;
        status: Scripts.Data.Game.Enums.GameStatus;
    }
    export class GameCreatedEvent extends Scripts.Data.Events.Game.GameInfo {
    }
    export class GameJoinedEvent extends Scripts.Data.Events.Game.GameInfo {
        players: Scripts.Data.Game.Player[];
    }
    export class GameStartedEvent extends Scripts.Data.Events.Game.GameInfo {
        players: Scripts.Data.Game.Player[];
    }
    export class GameFinishedEvent extends Scripts.Data.Events.Game.GameInfo {
    }
}
export module Scripts.Data.Events.Player {
    'use strict';
    export class PlayerInfo {
        id: string;
        name: string;
    }
    export class PlayerConnectedEvent extends Scripts.Data.Events.Player.PlayerInfo {
    }
    export class OtherPlayerConnectedEvent extends Scripts.Data.Events.Player.PlayerInfo {
    }
    export class PlayerDisconnectedEvent extends Scripts.Data.Events.Player.PlayerInfo {
    }
    export class OtherPlayerDisconnectedEvent extends Scripts.Data.Events.Player.PlayerInfo {
    }
}
export module Scripts.Data.Game {
    'use strict';
    export class Player {
        id: string;
        name: string;
    }
}
export module Scripts.Data.Game.Enums {
    'use strict';
    export enum GameStatus {
        Waiting = 1,
        Ongoing = 2,
        Ended = 3,
    }
}
