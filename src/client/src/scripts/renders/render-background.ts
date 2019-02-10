import Color from '../constants/color';

export default class RenderBackground {
    
    private static instance: RenderBackground;
    
    private canvas: HTMLCanvasElement;
    private ctx: CanvasRenderingContext2D;
    private backgroundImage: HTMLImageElement;
    private parent: HTMLDivElement;

    constructor() { }

    static getInstance() {
        if (!RenderBackground.instance) {
            RenderBackground.instance = new RenderBackground();
            RenderBackground.instance.parent = <HTMLDivElement>document.getElementById('game');
            
            // ... any one time initialization goes here ...
            RenderBackground.instance.canvas = <HTMLCanvasElement>document.getElementById('background-canvas');
            RenderBackground.instance.canvas.width = RenderBackground.instance.parent.offsetWidth;
            RenderBackground.instance.canvas.height = RenderBackground.instance.parent.offsetHeight;
            RenderBackground.instance.ctx = RenderBackground.instance.canvas.getContext('2d') as CanvasRenderingContext2D;

            RenderBackground.instance.recalculateMeasurements();
        }
        return RenderBackground.instance;
    }

    public recalculateMeasurements() {
        RenderBackground.instance.canvas.width = RenderBackground.instance.parent.offsetWidth;
        RenderBackground.instance.canvas.height = RenderBackground.instance.parent.offsetHeight;
    }

    public draw(): any {
        this.ctx.fillStyle = Color.sea;
        this.ctx.fillRect(0, 0, RenderBackground.instance.parent.offsetWidth, RenderBackground.instance.parent.offsetHeight);
    }
}