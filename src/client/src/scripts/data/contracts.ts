import Guid from './guid';
export module Scripts.Data.Game {
    'use strict';
    export class BoardGame {
        id: Guid;
        status: Scripts.Data.Game.Enums.GameStatus;
        rules: Scripts.Data.Game.Rules;
        players: System.Collections.Generic.KeyValuePair[];
        gameState: Scripts.Data.Game.GameState;
    }
    export class Rules {
        minPlayers: number;
        maxPlayers: number;
        pointsToWin: number;
        resources: Scripts.Data.Game.ResourceRuleSet[];
        numberSets: Scripts.Data.Game.NumberRuleSet[];
        ports: Scripts.Data.Game.PortRuleSet[];
        constructions: Scripts.Data.Game.ConstructionRuleSet[];
        developmentCardRuleSet: Scripts.Data.Game.DevelopmentCardRuleSet;
        effectCards: Scripts.Data.Game.EffectCardRuleSet[];
    }
    export class ResourceRuleSet {
        type: Scripts.Data.Game.Enums.ResourceType;
        color: string;
        amount: number;
    }
    export class NumberRuleSet {
        number: number;
        amount: number;
    }
    export class PortRuleSet {
        name: string;
        resourceType: Scripts.Data.Game.Enums.ResourceType;
        receive: number;
        pay: number;
        amount: number;
    }
    export class ConstructionRuleSet {
        type: Scripts.Data.Game.Enums.ConstructionType;
        cost: Scripts.Data.Game.ResourceRuleSet[];
    }
    export class DevelopmentCardRuleSet {
        cost: Scripts.Data.Game.ResourceRuleSet[];
        developmentCards: Scripts.Data.Game.DevelopmentCardTypeRuleSet[];
    }
    export class DevelopmentCardTypeRuleSet {
        type: Scripts.Data.Game.Enums.DevelopmentCardType;
        amount: number;
    }
    export class EffectCardRuleSet {
        name: string;
        extraPoints: number;
    }
    export class Player {
        id: string;
        name: string;
    }
    export class GameState {
        players: Scripts.Data.Game.GamePlayer[];
        board: Scripts.Data.Game.Board;
        developmentCards: Scripts.Data.Game.DevelopmentCard[];
        lastDiceRoll: number;
    }
    export class GamePlayer {
        player: Scripts.Data.Game.Player;
        game: Scripts.Data.Game.BoardGame;
        color: Scripts.Data.Game.Enums.PlayerColor;
        canMakeMove: boolean;
        points: number;
        resourceCards: Scripts.Data.Game.Enums.ResourceType[];
        developmentCards: Scripts.Data.Game.DevelopmentCard[];
        constructions: Scripts.Data.Game.Construction[];
        villages: Scripts.Data.Game.Construction[];
        cities: Scripts.Data.Game.Construction[];
        streets: Scripts.Data.Game.Construction[];
    }
    export class DevelopmentCard {
        type: Scripts.Data.Game.Enums.DevelopmentCardType;
        isPlayed: boolean;
    }
    export class Construction {
        type: Scripts.Data.Game.Enums.ConstructionType;
        coordinates: Scripts.Data.Libs.Hex[];
    }
    export class Board {
        tiles: Scripts.Data.Game.Tile[];
        ports: Scripts.Data.Game.Port[];
    }
    export class Tile {
        x: number;
        y: number;
        z: number;
        hex: Scripts.Data.Libs.Hex;
        resourceType: Scripts.Data.Game.Enums.ResourceType;
        value: number;
        isBlocked: boolean;
    }
    export class Port {
        name: string;
        resourceType: Scripts.Data.Game.Enums.ResourceType;
        hexes: Scripts.Data.Libs.Hex[];
    }
}
export module Scripts.Data.Game.Enums {
    'use strict';
    export enum GameStatus {
        Waiting = 1,
        Ongoing = 2,
        Ended = 3,
    }
    export enum ResourceType {
        Dessert = 0,
        Wood = 1,
        Brick = 2,
        Wool = 3,
        Wheat = 4,
        Ore = 5,
    }
    export enum ConstructionType {
        Street = 1,
        Village = 2,
        City = 3,
    }
    export enum DevelopmentCardType {
        Knight = 1,
        VictoryPoint = 2,
        Streets = 3,
        Resources = 4,
        Monopoly = 5,
    }
    export enum PlayerColor {
        Red = 1,
        Blue = 2,
        Gold = 3,
        White = 4,
    }
}
export module Scripts.Data.Libs {
    'use strict';
    export class Hex implements System.ICloneable {
        q: number;
        r: number;
        s: number;
        static directions: Scripts.Data.Libs.Hex[];
        static diagonals: Scripts.Data.Libs.Hex[];
    }
}
export module System {
    'use strict';
    export interface ICloneable {
    }
}
export module System.Collections.Generic {
    'use strict';
    export class KeyValuePair {
        key: string;
        value: Scripts.Data.Game.Player;
    }
}
