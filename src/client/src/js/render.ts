import Global from './global';

export default class Render {

    private static instance: Render;

    private canvas: HTMLCanvasElement;
    private ctx: CanvasRenderingContext2D;

    private textColor = '#333';
    private titelColor = '#f00';

    constructor() { }

    static getInstance() {
        if (!Render.instance) {
            Render.instance = new Render();
            
            // ... any one time initialization goes here ...
            Render.instance.canvas = <HTMLCanvasElement>document.getElementById('canvas');
            Render.instance.canvas.width = window.innerWidth;
            Render.instance.canvas.height = window.innerHeight;
            Render.instance.ctx = Render.instance.canvas.getContext('2d') as CanvasRenderingContext2D;
        }
        return Render.instance;
    }

    public clearGrid() {
        this.ctx.clearRect(0, 0, window.innerWidth, window.innerHeight);
    }

    public drawLoadingScreen() {
        this.drawText('Connecting to Catan server ...');
    }

    private drawText(text: string) {
        const x = window.innerWidth / 2;
        const y = window.innerHeight / 2;

        this.ctx.font = '30pt Roboto';
        this.ctx.textAlign = 'center';
        this.ctx.fillStyle = this.textColor;
        this.ctx.fillText(text, x, y);
    }
}