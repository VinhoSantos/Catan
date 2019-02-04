export default class RenderBackground {
    
    private static instance: RenderBackground;
    
    private canvas: HTMLCanvasElement;
    private ctx: CanvasRenderingContext2D;
    private backgroundImage: HTMLImageElement;

    constructor() { }

    static getInstance() {
        if (!RenderBackground.instance) {
            RenderBackground.instance = new RenderBackground();
            
            // ... any one time initialization goes here ...
            RenderBackground.instance.canvas = <HTMLCanvasElement>document.getElementById('background-canvas');
            RenderBackground.instance.canvas.width = window.innerWidth;
            RenderBackground.instance.canvas.height = window.innerHeight;
            RenderBackground.instance.ctx = RenderBackground.instance.canvas.getContext('2d') as CanvasRenderingContext2D;

            RenderBackground.instance.recalculateMeasurements();
        }
        return RenderBackground.instance;
    }

    public recalculateMeasurements() {
        RenderBackground.instance.canvas.width = window.innerWidth;
        RenderBackground.instance.canvas.height = window.innerHeight;
    }

    public drawBackground(): any {
        this.ctx.fillStyle = '#669cff'
        this.ctx.fillRect(0, 0, window.innerWidth, window.innerHeight);
    }
}