import Color from '../constants/color';
import { Hex, Layout, Point, Orientation } from '../libs/hexagon';

export default class RenderCanvas {

    private static instance: RenderCanvas;

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

    private disableMouseMoveTrigger: boolean = false;
    private previousHex: Hex;

    constructor() { }

    static getInstance() {
        if (!RenderCanvas.instance) {
            RenderCanvas.instance = new RenderCanvas();
            RenderCanvas.instance.parent = <HTMLDivElement>document.getElementById('game');
            
            // ... any one time initialization goes here ...
            RenderCanvas.instance.canvas = <HTMLCanvasElement>document.getElementById('game-canvas');
            RenderCanvas.instance.canvas.width = RenderCanvas.instance.parent.offsetWidth;
            RenderCanvas.instance.canvas.height = RenderCanvas.instance.parent.offsetHeight;
            RenderCanvas.instance.ctx = RenderCanvas.instance.canvas.getContext('2d') as CanvasRenderingContext2D;

            RenderCanvas.instance.recalculateMeasurements();
            RenderCanvas.instance.setDefaults();
        }
        return RenderCanvas.instance;
    }

    public recalculateMeasurements() {
        RenderCanvas.instance.canvas.width = RenderCanvas.instance.parent.offsetWidth;
        RenderCanvas.instance.canvas.height = RenderCanvas.instance.parent.offsetHeight;

        this.width = RenderCanvas.instance.parent.offsetWidth;
        this.height = RenderCanvas.instance.parent.offsetHeight;
        this.centerX = this.width / 2;
        this.centerY = this.height / 2;

        const maxSize = this.width < this.height ? this.width : this.height;
        this.hexR = maxSize / 9.5;
        this.hexWidth = Math.sqrt(3) * this.hexR;
        this.hexHeight = 2 * this.hexR;
        this.hexAltitude = this.hexWidth / 2;
        this.hexBorder = this.hexR / 5;
    }

    private setDefaults() {
        this.ctx.font = '10pt Roboto';
        this.ctx.textAlign = 'center';
        this.ctx.lineWidth = this.hexBorder;
        this.ctx.fillStyle = Color.text;
        this.ctx.strokeStyle = Color.text;
        this.ctx.save();
    }

    public clearScreen() {
        this.ctx.clearRect(0, 0, RenderCanvas.instance.parent.offsetWidth, RenderCanvas.instance.parent.offsetHeight);
    }

    public drawBoard() {       
        this.clearScreen();
        this.drawFields();
        this.drawStreets();
        this.drawVillages();
        this.drawCities();

        document.querySelector('#game-canvas').addEventListener('mousemove', (event: MouseEvent) => this.trackMouse(event));
    }  

    private trackMouse(event: MouseEvent): void {
        if (this.disableMouseMoveTrigger) return;

        this.disableMouseMoveTrigger = true;
        
        var mouseX = event.clientX;
        var mouseY = event.clientY;
        this.highlightHexagon(mouseX, mouseY);

        this.disableMouseMoveTrigger = false;
    }

    highlightHexagon(mouseX: number, mouseY: number): any {
        var layout = new Layout(Layout.pointy, new Point(this.hexR, this.hexR), new Point(this.centerX, this.centerY));
        var hex = layout.pixelToHex(new Point(mouseX, mouseY)).round();
        if (!this.previousHex) {
            this.previousHex = hex;
        } else {
            if (hex.q == this.previousHex.q && hex.r == this.previousHex.r && hex.s == this.previousHex.s) return;
        }

        console.log(hex.q + ' ' + hex.s + ' ' + hex.r);
        //console.log(`x, y, z: ${hex.q}, ${hex.s}, ${hex.r}`);
        //console.log(`distance: ${Math.round(hex.distance(new Hex(0, 0, 0)))}`);

        this.drawBoard();

        hex.neighbors().forEach((neighbor: Hex) => {
            this.drawHiglightedHexagon(neighbor);
        });

        hex.commonNeighborsWith(hex.neighbor(0)).forEach((neighbor: Hex) => {
            this.drawHiglightedHexagon(neighbor);
        });
        this.previousHex = hex;
    }

    private drawFields() {
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

    private drawStreets(): void {
        this.drawStreetAtJoint(new Hex(0, 0, 0), new Hex(1, 0, -1), 0);
        this.drawStreetAtJoint(new Hex(0, 0, 0), new Hex(0, 1, -1), 0);
        this.drawStreetAtJoint(new Hex(-1, 1, 0), new Hex(0, 1, -1), 0);
        this.drawStreetAtJoint(new Hex(-1, 2, -1), new Hex(0, 1, -1), 0);
        this.drawStreetAtJoint(new Hex(1, -1, 0), new Hex(0, 0, 0), 0);
    }

    private drawStreetAtJoint(field1: Hex, field2: Hex, playerIndex: number) {
        
        const field1X = this.centerX + (field1.q * this.hexWidth / 2) - (field1.s * this.hexWidth / 2);
        const field1Y = this.centerY + (field1.r * this.hexHeight * 3/4);        

        const field2X = this.centerX + (field2.q * this.hexWidth / 2) - (field2.s * this.hexWidth / 2);
        const field2Y = this.centerY + (field2.r * this.hexHeight * 3/4);
        
        //this.drawCircleAt(field1X, field1Y, 10, Color.start);
        //this.drawCircleAt(field2X, field2Y, 10, Color.end);
        //this.drawLine(new Point(field1X, field1Y), new Point(field2X, field2Y));

        let startX = 0;
        let startY = 0;
        let endX = 0;
        let endY = 0;

        if (field1.q === field2.q) { // x is gelijk
             if (field1.s < field2.s) { // y naar linksboven
                startX = field1X + this.hexR * Math.cos(this.toRadians(210));
                startY = field1Y + this.hexR * Math.sin(this.toRadians(210));
                endX = field1X + this.hexR * Math.cos(this.toRadians(270));
                endY = field1Y + this.hexR * Math.sin(this.toRadians(270));
             } else { //y naar rechtsonder
                startX = field1X + this.hexR * Math.cos(this.toRadians(30));
                startY = field1Y + this.hexR * Math.sin(this.toRadians(30));
                endX = field1X + this.hexR * Math.cos(this.toRadians(90));
                endY = field1Y + this.hexR * Math.sin(this.toRadians(90));
             }
        };

        if (field1.s === field2.s) { // y is gelijk
             if (field1.q > field2.q) { // x naar linksbeneden
                startX = field1X + this.hexR * Math.cos(this.toRadians(90));
                startY = field1Y + this.hexR * Math.sin(this.toRadians(90));
                endX = field1X + this.hexR * Math.cos(this.toRadians(150));
                endY = field1Y + this.hexR * Math.sin(this.toRadians(150));
             } else { // x naar rechtsboven
                startX = field1X + this.hexR * Math.cos(this.toRadians(270));
                startY = field1Y + this.hexR * Math.sin(this.toRadians(270));
                endX = field1X + this.hexR * Math.cos(this.toRadians(330));
                endY = field1Y + this.hexR * Math.sin(this.toRadians(330));
             }
        };

        if (field1.r === field2.r) { // z is gelijk
            if (field1.q > field2.q) { // x naar links
                startX = field1X + this.hexR * Math.cos(this.toRadians(150));
                startY = field1Y + this.hexR * Math.sin(this.toRadians(150));
                endX = field1X + this.hexR * Math.cos(this.toRadians(210));
                endY = field1Y + this.hexR * Math.sin(this.toRadians(210));
            } else { // x naar rechts
                startX = field1X + this.hexR * Math.cos(this.toRadians(330));
                startY = field1Y + this.hexR * Math.sin(this.toRadians(330));
                endX = field1X + this.hexR * Math.cos(this.toRadians(30));
                endY = field1Y + this.hexR * Math.sin(this.toRadians(30));
            }             
        };        
        
        this.drawLine(new Point(startX, startY), new Point(endX, endY), this.hexBorder / 2, Color.players[playerIndex]);
    }

    private drawVillages() {
        this.drawVillageAtIntersection(new Hex(0, 0, 0), new Hex(1, -1, 0), new Hex(1, 0, -1), 0); //[0,0,0],[1,0,-1],[1,-1,0]);
    }

    private drawVillageAtIntersection(field1: Hex, field2: Hex, field3: Hex, playerIndex: number) {        
        var intersection = this.getIntersection(field1, field2, field3);
        this.drawCircleAt(intersection.x, intersection.y, this.hexR / 5, Color.players[playerIndex]);
    }

    private drawCities() {
        this.drawCityAtIntersection(new Hex(-1, 2, -1), new Hex(0, 1, -1), new Hex(-1, 1, 0), 0);
    }

    private drawCityAtIntersection(field1: Hex, field2: Hex, field3: Hex, playerIndex: number) {        
        var intersection = this.getIntersection(field1, field2, field3);
        this.drawSquareAt(intersection.x - this.hexR / 5, intersection.y - this.hexR / 5, this.hexR * 2/5, Color.players[playerIndex]);
    }

    getIntersection(field1: Hex, field2: Hex, field3: Hex): Point {
        const field1X = this.centerX + (field1.q * this.hexWidth / 2) - (field1.s * this.hexWidth / 2);
        const field1Y = this.centerY + (field1.r * this.hexHeight * 3 / 4);
               
        const field2X = this.centerX + (field2.q * this.hexWidth / 2) - (field2.s * this.hexWidth / 2);
        const field2Y = this.centerY + (field2.r * this.hexHeight * 3 / 4);
        
        const field3X = this.centerX + (field3.q * this.hexWidth / 2) - (field3.s * this.hexWidth / 2);
        const field3Y = this.centerY + (field3.r * this.hexHeight * 3 / 4);
                
        // this.ctx.restore();
        // this.ctx.beginPath();
        // this.ctx.moveTo(field1X, field1Y);
        // this.ctx.lineTo(field2X, field2Y);
        // this.ctx.lineTo(field3X, field3Y);
        // this.ctx.closePath();
        // this.ctx.stroke();

        //calculate intersection
        const intersectionField1And2X = field1X + ((field2X - field1X) / 2);
        const intersectionField1And2Y = field1Y + ((field2Y - field1Y) / 2);
        
        const intersectionX = intersectionField1And2X + ((field3X - intersectionField1And2X) / 3);
        const intersectionY = intersectionField1And2Y + ((field3Y - intersectionField1And2Y) / 3);

        return new Point(intersectionX, intersectionY);
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

    private drawHiglightedHexagon(hex: Hex) {
        //calculate middle point of hexagon
        const posX = this.centerX + (hex.q * this.hexWidth / 2) - (hex.s * this.hexWidth / 2);
        const posY = this.centerY + (hex.r * this.hexHeight * 3 / 4);

        //draw hexagon
        const point = new Point(posX, posY);
        this.drawHexagonAtCoordinates(new Point(posX, posY), this.hexR - (this.hexBorder / 2));

        this.ctx.fillStyle = 'rgba(255, 255, 255, 0.4)';
        this.ctx.fill(); 
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

    private toDegrees (angle: number): number {
        return angle * (180 / Math.PI);
    }

    private toRadians (angle: number): number {
        return angle * (Math.PI / 180);
    }

    public redraw() {
        this.drawBoard();
    }

    private drawCircleAt(x: number, y: number, radius: number = 10, color: string = Color.text) {
        this.ctx.restore();
        this.ctx.beginPath();
        this.ctx.arc(x, y, radius, 0, 2 * Math.PI);
        this.ctx.fillStyle = color;
        this.ctx.fill();
    }

    private drawSquareAt(x: number, y: number, width: number, color: string = Color.text) {
        this.drawRectangleAt(x, y, width, width, color);
    }

    private drawRectangleAt(x: number, y: number, width: number, height: number, color: string = Color.text) {
        this.ctx.restore();
        this.ctx.fillStyle = color;
        this.ctx.fillRect(x, y, width, height);
    }

    private drawLine(from: Point, to: Point, lineWidth: number = this.hexBorder, color: string = Color.text): void {
        this.ctx.restore();
        this.ctx.beginPath();
        this.ctx.moveTo(from.x, from.y);        
        this.ctx.lineTo(to.x, to.y);
        this.ctx.lineWidth = lineWidth;
        this.ctx.strokeStyle = color;
        this.ctx.stroke();
    }
    
    private drawTextAt(text: string, point: Point) {
        this.ctx.restore();
        this.ctx.font = '30pt Roboto';
        this.ctx.textAlign = 'center';
        this.ctx.fillStyle = Color.text;
        this.ctx.fillText(text, point.x, point.y);
    }
}