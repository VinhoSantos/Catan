import * as $ from 'jquery';

export default class RenderDom {

    private readonly el: JQuery<HTMLElement>;
    
    constructor(selector: string) {
        this.el = $(selector);
    }

    public show(): void {
        this.el.show();
    }

    public hide(): void {
        this.el.hide();
    }

    public write(text: string): void {
        this.el.text(text);
    }

    public set(value: string): void {
        this.el.val(value);
    }

}