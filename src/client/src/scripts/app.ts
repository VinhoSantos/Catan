import '../css/main.scss';

import Game from './catan';

class App {
    constructor(private readonly game: Game) {}

    public init(): void {
        this.game.start();
    }
}

window.onload = () => {
    const app = new App(new Game());

    app.init();
}