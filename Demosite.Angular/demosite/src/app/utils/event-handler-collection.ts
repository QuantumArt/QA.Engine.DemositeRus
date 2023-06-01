export interface EventHandler {
  element: Element;
  type: string;
  action: (event: Event) => void;
  useCapture?: boolean;
}

export class EventHandlerCollection {
  private handlers: EventHandler[] = [];

  public add(handler: EventHandler): void {
    handler.element.addEventListener(handler.type, handler.action, handler.useCapture ?? false);
    this.handlers.push(handler);
  }

  public removeAll(): void {
    this.handlers.forEach(handler =>
      handler.element.removeEventListener(handler.type, handler.action, handler.useCapture ?? false),
    );
    this.handlers = [];
  }
}
