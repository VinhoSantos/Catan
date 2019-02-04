export module Scripts.Data.Libs {
    'use strict';
    export class Hex {
        q: number;
        r: number;
        s: number;
        static directions: Scripts.Data.Libs.Hex[];
        static diagonals: Scripts.Data.Libs.Hex[];
    }
}
export module Scripts.Data.Models {
    'use strict';
    export class GameState {
        players: Scripts.Data.Models.Player[];
        rules: Scripts.Data.Models.Rules;
        board: Scripts.Data.Models.Board;
    }
    export class Player {
        id: string;
        name: string;
    }
    export class Rules {
        minPlayers: number;
        maxPlayers: number;
        pointsToWin: number;
        resources: Scripts.Data.Models.ResourceRuleSet[];
        numberSets: Scripts.Data.Models.NumberRuleSet[];
        ports: Scripts.Data.Models.PortRuleSet[];
        constructions: Scripts.Data.Models.ConstructionRuleSet[];
        developmentCards: Scripts.Data.Models.DevelopmentCardRuleSet[];
        effectCards: Scripts.Data.Models.EffectCardRuleSet[];
    }
    export class ResourceRuleSet {
        type: Scripts.Data.Models.Enums.ResourceType;
        color: string;
        amount: number;
    }
    export class NumberRuleSet {
        number: number;
        amount: number;
    }
    export class PortRuleSet {
        name: string;
        resourceType: Scripts.Data.Models.Enums.ResourceType;
        receive: number;
        pay: number;
        amount: number;
    }
    export class ConstructionRuleSet {
        type: Scripts.Data.Models.Enums.ConstructionType;
        cost: Scripts.Data.Models.ResourceRuleSet[];
    }
    export class DevelopmentCardRuleSet {
        type: Scripts.Data.Models.Enums.DevelopmentCardType;
        amount: number;
    }
    export class EffectCardRuleSet {
        name: string;
        extraPoints: number;
    }
    export class Board {
        tiles: Scripts.Data.Models.Tile[];
        ports: Scripts.Data.Models.Port[];
        constructions: Scripts.Data.Models.Construction[];
    }
    export class Tile {
        x: number;
        y: number;
        z: number;
        hex: Scripts.Data.Libs.Hex;
        resourceType: Scripts.Data.Models.Enums.ResourceType;
        value: number;
    }
    export class Port {
        name: string;
        resourceType: Scripts.Data.Models.Enums.ResourceType;
        hexes: Scripts.Data.Libs.Hex[];
    }
    export class Construction {
        type: Scripts.Data.Models.Enums.ConstructionType;
        hexes: Scripts.Data.Libs.Hex[];
    }
}
export module Scripts.Data.Models.Enums {
    'use strict';
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
        DevelopmentCard = 4,
    }
    export enum DevelopmentCardType {
        Knight = 1,
        VictoryPoint = 2,
        Streets = 3,
        Resources = 4,
        Monopoly = 5,
    }
}
