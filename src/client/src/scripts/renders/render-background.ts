import Color from '../constants/color';
import { Hex, Layout, Point, Orientation } from '../libs/hexagon';

export default class RenderBackground {
    
    private static instance: RenderBackground;
    
    private canvas: HTMLCanvasElement;
    private ctx: CanvasRenderingContext2D;
    private backgroundImage: HTMLImageElement;
    private parent: HTMLDivElement;

    private width: number;
    private height: number;
    private centerX: number;
    private centerY: number;
    private hexR: number; //radius of hexagon (distance from the center to a corner)
    private hexAltitude: number; //altitude of the hexagon (perpendicular distance from the center to a side = 90° angle)
    private hexWidth: number; //max width of a hexagon
    private hexHeight: number; //max height of a hexagon
    private hexBorder: number; //border of hexagon

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
        
        this.width = RenderBackground.instance.parent.offsetWidth;
        this.height = RenderBackground.instance.parent.offsetHeight;
        this.centerX = this.width / 2;
        this.centerY = this.height / 2;

        const maxSize = this.width < this.height ? this.width : this.height;
        this.hexR = maxSize / 9.5;
        this.hexWidth = Math.sqrt(3) * this.hexR;
        this.hexHeight = 2 * this.hexR;
        this.hexAltitude = this.hexWidth / 2;
        this.hexBorder = this.hexR / 5;
    }

    public draw(): any {
        this.drawSea();
        this.drawBoard();
    }

    public drawSea() {
        this.ctx.fillStyle = Color.sea;
        this.ctx.fillRect(0, 0, RenderBackground.instance.parent.offsetWidth, RenderBackground.instance.parent.offsetHeight);
    }

    public drawBoard() {
        this.drawResourceFields();
    }

    private drawResourceFields() {
        //core
        this.drawHexagon(new Hex(0, 0, 0), Color.dessert);
        //inner ring
        this.drawHexagon(new Hex(1, 0, -1), Color.wood);
        this.drawHexagon(new Hex(0, 1, -1), Color.wool);
        this.drawHexagon(new Hex(-1, 1, 0), Color.wheat);
        this.drawHexagon(new Hex(-1, 0, 1), Color.wood);
        this.drawHexagon(new Hex(0, -1, 1), Color.brick);
        this.drawHexagon(new Hex(1, -1, 0), Color.wool);
        //outer ring
        this.drawHexagon(new Hex(2, 0, -2), Color.wheat);
        this.drawHexagon(new Hex(1, 1, -2), Color.ore);
        this.drawHexagon(new Hex(0, 2, -2), Color.wood);
        this.drawHexagon(new Hex(-1, 2, -1), Color.brick);
        this.drawHexagon(new Hex(-2, 2, 0), Color.wool);
        this.drawHexagon(new Hex(-2, 1, 1), Color.wheat);
        this.drawHexagon(new Hex(-2, 0, 2), Color.ore);
        this.drawHexagon(new Hex(-1, -1, 2), Color.wood);
        this.drawHexagon(new Hex(0, -2, 2), Color.brick);
        this.drawHexagon(new Hex(1, -2, 1), Color.wool);
        this.drawHexagon(new Hex(2, -2, 0), Color.wheat);
        this.drawHexagon(new Hex(2, -1, -1), Color.ore);
    }

    private drawHexagon(hex: Hex, fieldType: string) {
        //calculate middle point of hexagon
        const posX = this.centerX + (hex.q * this.hexWidth / 2) - (hex.s * this.hexWidth / 2);
        const posY = this.centerY + (hex.r * this.hexHeight * 3 / 4);

        //draw hexagon
        const point = new Point(posX, posY);
        this.drawHexagonAtCoordinates(new Point(posX, posY), this.hexR);

        this.ctx.fillStyle = fieldType;
        this.ctx.fill();
        this.ctx.strokeStyle = Color.border;
        this.ctx.lineWidth = this.hexBorder;
        this.ctx.stroke();   

        //draw dot in center of hexagon
        this.drawCircleAt(point.x, point.y, this.hexR / 20, Color.hexCenter);

        //draw x, y, z coordinates in hexagon
        this.ctx.font = '10pt Roboto';
        this.ctx.textAlign = 'center';
        this.ctx.fillStyle = Color.text;

        var textX = `${hex.q}`;
        var textY = `${hex.s}`;
        var textZ = `${hex.r}`;

        if (hex.q === 0 && hex.s === 0 && hex.r === 0) {
            textX = `x = q`;
            textY = `y = s`;
            textZ = `z = r`;
        }

        this.ctx.fillText(textX, point.x + (this.hexR * 3/5) * Math.cos(this.toRadians(330)), point.y + (this.hexR * 3/5) * Math.sin(this.toRadians(330)));
        this.ctx.fillText(textY, point.x + (this.hexR * 3/5) * Math.cos(this.toRadians(210)), point.y + (this.hexR * 3/5) * Math.sin(this.toRadians(210)));
        this.ctx.fillText(textZ, point.x + (this.hexR * 2/3) * Math.cos(this.toRadians(90)), point.y + (this.hexR * 2/3) * Math.sin(this.toRadians(90)));
    }

    private drawHexagonAtCoordinates(point: Point, size: number) {
        this.ctx.beginPath();
        this.ctx.moveTo(point.x + size * Math.cos(this.toRadians(30)), point.y + size * Math.sin(this.toRadians(30))); //punt rechtsonder
        this.ctx.lineTo(point.x + size * Math.cos(this.toRadians(90)), point.y + size * Math.sin(this.toRadians(90)));
        this.ctx.lineTo(point.x + size * Math.cos(this.toRadians(150)), point.y + size * Math.sin(this.toRadians(150)));
        this.ctx.lineTo(point.x + size * Math.cos(this.toRadians(210)), point.y + size * Math.sin(this.toRadians(210)));
        this.ctx.lineTo(point.x + size * Math.cos(this.toRadians(270)), point.y + size * Math.sin(this.toRadians(270)));
        this.ctx.lineTo(point.x + size * Math.cos(this.toRadians(330)), point.y + size * Math.sin(this.toRadians(330)));
        this.ctx.closePath();            
    }

    private drawCircleAt(x: number, y: number, radius: number = 10, color: string = Color.text) {
        this.ctx.restore();
        this.ctx.beginPath();
        this.ctx.arc(x, y, radius, 0, 2 * Math.PI);
        this.ctx.fillStyle = color;
        this.ctx.fill();
    }

    private toRadians (angle: number): number {
        return angle * (Math.PI / 180);
    }
}