export class OrderItem {
    id: number = 0;
    quantity: number = 1;
    unitPrice: number = 1;
    productId: number = 1;
    productCategory: string = "";
    productPrice: number = 0;
    productTitle: string = "";
    productArtId: string = "";
    productArtist: string = "";
}

export class Order {
    orderId: number = 0;
    orderDate: Date = new Date();
    orderNumber: string = Math.random().toString(36).substr(2,7);
    items: OrderItem[] = [];

    get subtotal(): number {
        const result = this.items.reduce(
            (total, value) => { return total + (value.unitPrice * value.quantity); }, 0);
        return result;
    }
}