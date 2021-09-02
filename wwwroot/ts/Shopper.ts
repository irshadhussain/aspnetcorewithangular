class Shopper { 
    constructor(private firstName, private lastName) {

    }

    showName() {
        alert(`${this.firstName} ${this.lastName}`);
    }
}