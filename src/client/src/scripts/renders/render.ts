export default class Render {

    private static instance: Render;

    private canvas: HTMLCanvasElement;
    private ctx: CanvasRenderingContext2D;
    private backgroundImage: HTMLImageElement;

    private width: number;
    private height: number;
    private centerX: number;
    private centerY: number;
    private hexR: number; //radius of hexagon (distance from the center to a corner)
    private hexWidth: number; //max width of a hexagon
    private hexHeight: number; //max height of a hexagon

    private textColor = '#333';

    private fieldColors = ['#c2b280', '#228B22', '#dc5539', '#e1e0d8', '#f5deb3', '#999'];

    constructor() { }

    static getInstance() {
        if (!Render.instance) {
            Render.instance = new Render();
            
            // ... any one time initialization goes here ...
            Render.instance.canvas = <HTMLCanvasElement>document.getElementById('game-canvas');
            Render.instance.canvas.width = window.innerWidth;
            Render.instance.canvas.height = window.innerHeight;
            Render.instance.ctx = Render.instance.canvas.getContext('2d') as CanvasRenderingContext2D;

            Render.instance.recalculateMeasurements();
        }
        return Render.instance;
    }

    public recalculateMeasurements() {
        Render.instance.canvas.width = window.innerWidth;
        Render.instance.canvas.height = window.innerHeight;

        this.width = window.innerWidth;
        this.height = window.innerHeight;
        this.centerX = this.width / 2;
        this.centerY = this.height / 2;

        const maxSize = this.width < this.height ? this.width : this.height;
        this.hexR = maxSize / 10;

        this.hexWidth = Math.sqrt(3) * this.hexR;
        this.hexHeight = 2 * this.hexR;
    }

    public clearScreen() {
        this.ctx.clearRect(0, 0, window.innerWidth, window.innerHeight);
    }

    public drawLoadingScreen() {
        this.clearScreen();
        this.drawText('Connecting to Catan server ...');
    }

    public drawBoard() {       
        this.clearScreen();
        this.drawFields();
        this.drawVillages();
    }    

    private drawFields() {
        //core
        this.drawHexagonAtCoordinates(0, 0, 0);
        //inner ring
        this.drawHexagonAtCoordinates(1, -1, 0);
        this.drawHexagonAtCoordinates(0, -1, 1);
        this.drawHexagonAtCoordinates(-1, 0, 1);
        this.drawHexagonAtCoordinates(-1, 1, 0);
        this.drawHexagonAtCoordinates(0, 1, -1);
        this.drawHexagonAtCoordinates(1, 0, -1);
        //outer ring
        this.drawHexagonAtCoordinates(2, -2, 0);
        this.drawHexagonAtCoordinates(1, -2, 1);
        this.drawHexagonAtCoordinates(0, -2, 2);
        this.drawHexagonAtCoordinates(-1, -1, 2);
        this.drawHexagonAtCoordinates(-2, 0, 2);
        this.drawHexagonAtCoordinates(-2, 1, 1);
        this.drawHexagonAtCoordinates(-2, 2, 0);
        this.drawHexagonAtCoordinates(-1, 2, -1);
        this.drawHexagonAtCoordinates(0, 2, -2);
        this.drawHexagonAtCoordinates(1, 1, -2);
        this.drawHexagonAtCoordinates(2, 0, -2);
        this.drawHexagonAtCoordinates(2, -1, -1);
    }

    private drawVillages() {
        this.drawVillageAtIntersection([0,0,0],[1,0,-1],[1,-1,0]);
    }

    private drawVillageAtIntersection(field1: number[], field2: number[], field3: number[]) {
        
        this.ctx.beginPath();
        const field1X = this.centerX + (field1[0] * this.hexWidth / 2) - (field1[1] * this.hexWidth / 2);
        const field1Y = this.centerY + (field1[2] * this.hexHeight * 3 / 4);
        this.ctx.moveTo(field1X, field1Y);

        const field2X = this.centerX + (field2[0] * this.hexWidth / 2) - (field2[1] * this.hexWidth / 2);
        const field2Y = this.centerY + (field2[2] * this.hexHeight * 3 / 4);
        this.ctx.lineTo(field2X, field2Y);

        const field3X = this.centerX + (field3[0] * this.hexWidth / 2) - (field3[1] * this.hexWidth / 2);
        const field3Y = this.centerY + (field3[2] * this.hexHeight * 3 / 4);
        this.ctx.lineTo(field3X, field3Y);
        
        this.ctx.closePath();
        this.ctx.stroke();

        //calculate intersection
        const intersectionField1And2X = field1X + ((field2X - field1X) / 2);
        const intersectionField1And2Y = field1Y + ((field2Y - field1Y) / 2);
        
        const intersectionX = intersectionField1And2X + ((field3X - intersectionField1And2X) / 3);
        const intersectionY = intersectionField1And2Y + ((field3Y - intersectionField1And2Y) / 3);

        this.ctx.beginPath();
        this.ctx.arc(intersectionX, intersectionY, this.hexR / 10, 0, 2 * Math.PI);
        this.ctx.fill();
    }

    private drawHexagonAtCoordinates(coX: number, coY: number, coZ: number) {
        //calculate middle point of hexagon
        const posX = this.centerX + (coX * this.hexWidth / 2) - (coY * this.hexWidth / 2);
        const posY = this.centerY + (coZ * this.hexHeight * 3 / 4);

        //draw hexagon
        this.ctx.beginPath();
        this.ctx.moveTo(posX + this.hexR * Math.cos(this.toRadians(30)), posY + this.hexR * Math.sin(this.toRadians(30)));
        this.ctx.lineTo(posX + this.hexR * Math.cos(this.toRadians(90)), posY + this.hexR * Math.sin(this.toRadians(90)));
        this.ctx.lineTo(posX + this.hexR * Math.cos(this.toRadians(150)), posY + this.hexR * Math.sin(this.toRadians(150)));
        this.ctx.lineTo(posX + this.hexR * Math.cos(this.toRadians(210)), posY + this.hexR * Math.sin(this.toRadians(210)));
        this.ctx.lineTo(posX + this.hexR * Math.cos(this.toRadians(270)), posY + this.hexR * Math.sin(this.toRadians(270)));
        this.ctx.lineTo(posX + this.hexR * Math.cos(this.toRadians(330)), posY + this.hexR * Math.sin(this.toRadians(330)));
        this.ctx.closePath();        
        this.ctx.fillStyle = this.fieldColors[Math.floor(Math.random() * 6)];
        this.ctx.fill();
        this.ctx.strokeStyle = '#888';
        this.ctx.stroke();

        //draw dot in center of hexagon
        this.ctx.beginPath();
        this.ctx.arc(posX, posY, this.hexR / 15, 0, 2 * Math.PI);
        this.ctx.fillStyle = '#bf0000';
        this.ctx.fill();

        //draw x, y, z coordinates in hexagon
        this.ctx.font = '10pt Roboto';
        this.ctx.textAlign = 'center';
        this.ctx.fillStyle = this.textColor;

        var textX = `${coX}`;
        var textY = `${coY}`;
        var textZ = `${coZ}`;

        if (coX === 0 && coY === 0 && coZ === 0) {
            textX = `x = ${textX}`;
            textY = `y = ${textY}`;
            textZ = `z = ${textZ}`;
        }

        this.ctx.fillText(textX, posX + (this.hexR * 2/3) * Math.cos(this.toRadians(330)), posY + (this.hexR * 2/3) * Math.sin(this.toRadians(330)));
        this.ctx.fillText(textY, posX + (this.hexR * 2/3) * Math.cos(this.toRadians(210)), posY + (this.hexR * 2/3) * Math.sin(this.toRadians(210)));
        this.ctx.fillText(textZ, posX + (this.hexR * 2/3) * Math.cos(this.toRadians(90)), posY + (this.hexR * 2/3) * Math.sin(this.toRadians(90)));
    }

    private drawText(text: string) {
        const x = window.innerWidth / 2;
        const y = window.innerHeight / 2;

        this.ctx.font = '30pt Roboto';
        this.ctx.textAlign = 'center';
        this.ctx.fillStyle = this.textColor;
        this.ctx.fillText(text, x, y);
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
}