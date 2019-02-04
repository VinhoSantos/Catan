export default class Loader {

    private readonly el: HTMLElement;
    
    constructor() {
        this.el = document.getElementById('loader');
    }

    public show(): void {
        this.el.style.display = 'block';
    }

    public hide(): void {
        this.el.style.display = 'none;'
    }

    public write(text: string): void {
        this.el.textContent = text;
    }

}