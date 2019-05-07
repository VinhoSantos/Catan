import RenderCanvas from './render-canvas';
import RenderBackground from './render-background';
import RenderDom from './render-dom';

export default class Drawer {

    private background: RenderBackground;
    private canvas: RenderCanvas;
    private loader: RenderDom;
    
    constructor() {
        this.background = RenderBackground.getInstance();
        this.canvas = RenderCanvas.getInstance();
        this.loader = new RenderDom('#loader');
    }

    public drawLoadingScreen(): void {
        this.loader.write('Loading Catan...');
        this.loader.show();
    }

    public drawHomeScreen(): void {
        this.background.draw();
        this.canvas.drawBoard();
        this.loader.hide();   
    }

    public resize(): any {
        this.background.recalculateMeasurements();
        this.background.draw();
        this.canvas.recalculateMeasurements();
        this.canvas.drawBoard();
    }
}