import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map } from 'rxjs/operators'
import { LoginRequest, LoginResult } from "../shared/LoginResult";
import { Order, OrderItem } from "../shared/Order";
import { Product } from "../shared/Product";

@Injectable()
export class Store {
    public order: Order = new Order();
    public products: Product[] = [];
    public token: string = "";
    public expiration: Date = new Date();

    constructor(private httpClient: HttpClient) { }

    get loginRequired(): boolean {
        return this.token.length === 0 || this.expiration < new Date();
    }

    login(creds: LoginRequest) {
        return this.httpClient.post<LoginResult>("/account/createtoken", creds)
            .pipe(map(data => {
                this.token = data.token;
                this.expiration = data.expiration;
            }));
    }

    loadProducts(): Observable<void> {
        return this.httpClient.get<[]>('/api/products')
            .pipe(map(data => {
                this.products = data;
                return;
            })
            );
    }

    addToOrder(product: Product) {

        let item: OrderItem | undefined;
        item = this.order.items.find(i => { i.productId === product.id });
        if (item) {
            item.quantity++;
        }
        else {
            item = new OrderItem();
            item.productId = product.id;
            item.productTitle = product.title;
            item.productCategory = product.category;
            item.productPrice = product.price;
            item.productArtId = product.artId;
            item.productArtist = product.artist;
            item.productTitle = product.title;
            item.unitPrice = product.price;
            item.quantity = 1;

            this.order.items.push(item);
        }
    }

    checkOut() {
        const headers = new HttpHeaders().set("Authorization", `Bearer ${this.token}`);
        return this.httpClient.post('/api/orders/', this.order
            , { headers: headers })
            .pipe(map(() => {
                this.order = new Order();
            }
            ));
    }
}