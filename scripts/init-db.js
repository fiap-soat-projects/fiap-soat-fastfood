db = db.getSiblingDB('fastfood');

db.createUser({
    user: "{{username}",
    pwd: "{{password}}",
    roles: [{ role: "readWrite", db: "fastfood" }]
});

const now = new Date();

db.customer.createIndex({ cpf: 1 }, { unique: true });

db.customer.insertMany([
    {
        "_id": ObjectId("68d076b6020449f7b3d993e6"),
        "createdAt": "2025-09-21T22:05:42.4275297Z",
        "name": "alison",
        "cpf": "11122233377",
        "email": "alison@test.com"
    },
    {
        "_id": ObjectId("68d07795a84df29051d120e8"),
        "createdAt": "2025-09-21T22:05:42.4275297Z",
        "name": "jeferson",
        "cpf": "11122333377",
        "email": "jeferson@test.com"
    },
    {
        "_id": ObjectId("68d0779da84df29051d120e9"),
        "createdAt": "2025-09-21T22:05:42.4275297Z",
        "name": "jamison",
        "cpf": "11122333379",
        "email": "jeferson@test.com"
    }
]);

db.menu.createIndex({ name: 1 }, { unique: true });

db.menu.insertMany([
    {
        _id: ObjectId('68d069e748439d63f889b03d'),
        createdAt: now,
        name: "X-Burguer",
        price: 15.90,
        description: "Hambúrguer com queijo, alface, tomate e maionese.",
        category: "MainCourse",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "X-Salada",
        price: 17.50,
        description: "Hambúrguer com queijo, salada e molho especial.",
        category: "MainCourse",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "X-Bacon",
        price: 18.90,
        description: "Hambúrguer com queijo, bacon crocante e molho da casa.",
        category: "MainCourse",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "X-Egg",
        price: 19.50,
        description: "Hambúrguer com ovo frito, queijo e alface.",
        category: "MainCourse",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "X-Frango",
        price: 16.90,
        description: "Sanduíche de frango grelhado com alface e maionese.",
        category: "MainCourse",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Cheddar Melt",
        price: 20.00,
        description: "Hambúrguer com queijo cheddar derretido e cebola caramelizada.",
        category: "MainCourse",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Vegetariano",
        price: 17.00,
        description: "Sanduíche com hambúrguer de grão-de-bico e vegetais.",
        category: "MainCourse",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "X-Calabresa",
        price: 18.50,
        description: "Hambúrguer com calabresa fatiada e queijo.",
        category: "MainCourse",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Duplo Burguer",
        price: 22.90,
        description: "Dois hambúrgueres, queijo duplo e molho especial.",
        category: "MainCourse",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Mini Burguer",
        price: 10.00,
        description: "Mini hambúrguer ideal para crianças.",
        category: "MainCourse",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Batata Frita Pequena",
        price: 6.00,
        description: "Porção pequena de batata frita.",
        category: "SideDish",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Batata Frita Média",
        price: 9.00,
        description: "Porção média de batata frita.",
        category: "SideDish",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Batata Frita Grande",
        price: 12.00,
        description: "Porção grande de batata frita.",
        category: "SideDish",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Onion Rings",
        price: 10.00,
        description: "Anéis de cebola empanados e fritos.",
        category: "SideDish",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Nuggets de Frango",
        price: 11.00,
        description: "Porção com 6 nuggets crocantes.",
        category: "SideDish",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Queijo Empanado",
        price: 12.50,
        description: "Cubos de queijo empanado e frito.",
        category: "SideDish",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Mini Salada",
        price: 7.50,
        description: "Salada leve com alface, tomate e cenoura.",
        category: "SideDish",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Molho Barbecue",
        price: 2.00,
        description: "Pote individual de molho barbecue.",
        category: "SideDish",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Molho Cheddar",
        price: 2.50,
        description: "Pote individual de molho cheddar.",
        category: "SideDish",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Farofa de Bacon",
        price: 4.00,
        description: "Farofa temperada com pedaços de bacon.",
        category: "SideDish",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Refrigerante Lata",
        price: 5.00,
        description: "Lata de refrigerante 350ml.",
        category: "Beverage",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Refrigerante 600ml",
        price: 7.00,
        description: "Garrafa de refrigerante 600ml.",
        category: "Beverage",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Água Mineral",
        price: 3.00,
        description: "Garrafa de água sem gás 500ml.",
        category: "Beverage",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Suco Natural",
        price: 8.00,
        description: "Copo de suco natural da fruta (laranja, limão ou maracujá).",
        category: "Beverage",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Chá Gelado",
        price: 6.50,
        description: "Chá gelado de limão ou pêssego.",
        category: "Beverage",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Milkshake Chocolate",
        price: 10.00,
        description: "Milkshake de chocolate 300ml.",
        category: "Beverage",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Milkshake Morango",
        price: 10.00,
        description: "Milkshake de morango 300ml.",
        category: "Beverage",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Cerveja Long Neck",
        price: 9.00,
        description: "Cerveja long neck 330ml.",
        category: "Beverage",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Energético",
        price: 12.00,
        description: "Lata de energético 250ml.",
        category: "Beverage",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Café Expresso",
        price: 4.50,
        description: "Café expresso pequeno.",
        category: "Beverage",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Pudim de Leite",
        price: 6.00,
        description: "Fatia de pudim tradicional.",
        category: "Dessert",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Mousse de Maracujá",
        price: 5.50,
        description: "Taça de mousse cremosa de maracujá.",
        category: "Dessert",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Brownie",
        price: 7.00,
        description: "Brownie de chocolate com nozes.",
        category: "Dessert",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Sorvete 2 bolas",
        price: 8.00,
        description: "Taça com 2 bolas de sorvete.",
        category: "Dessert",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Petit Gâteau",
        price: 12.00,
        description: "Bolo de chocolate com recheio quente e sorvete.",
        category: "Dessert",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Torta de Limão",
        price: 6.50,
        description: "Fatia de torta com cobertura merengue.",
        category: "Dessert",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Brigadeiro Gourmet",
        price: 3.00,
        description: "Unidade de brigadeiro artesanal.",
        category: "Dessert",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Banana Caramelada",
        price: 7.50,
        description: "Banana quente com calda de caramelo.",
        category: "Dessert",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Torta de Maçã",
        price: 7.00,
        description: "Fatia de torta de maçã com canela.",
        category: "Dessert",
        isActive: true,
        isDeleted: false
    },
    {
        _id: ObjectId(),
        createdAt: now,
        name: "Creme de Papaya",
        price: 8.00,
        description: "Creme de mamão com licor.",
        category: "Dessert",
        isActive: true,
        isDeleted: false
    }
]);

db.order.insertMany([
    {
        "_id": ObjectId(),
        "customerId": "",
        "customerName": "",
        "items": [
            {
                "_id": "68d069e748439d63f889b03d",
                "name": "X-Burguer",
                "category": "MainCourse",
                "price": 15.9,
                "amount": 3
            }
        ],
        "status": "Pending",
        "payment": {
            "method": "None",
            "status": "None"
        },
        "totalPrice": 47.7
    },
    {
        "_id": ObjectId(),
        "customerId": "",
        "customerName": "",
        "items": [
            {
                "_id": "68d069e748439d63f889b03d",
                "name": "X-Burguer",
                "category": "MainCourse",
                "price": 15.9,
                "amount": 3
            }
        ],
        "status": "Received",
        "payment": {
            "method": "Pix",
            "status": "Authorized"
        },
        "totalPrice": 47.7
    },
    {
        "_id": ObjectId(),
        "customerId": "",
        "customerName": "",
        "items": [
            {
                "_id": "68d069e748439d63f889b03d",
                "name": "X-Burguer",
                "category": "MainCourse",
                "price": 15.9,
                "amount": 3
            }
        ],
        "status": "InProgress",
        "payment": {
            "method": "Pix",
            "status": "Authorized"
        },
        "totalPrice": 47.7
    },
    {
        "_id": ObjectId(),
        "customerId": "",
        "customerName": "",
        "items": [
            {
                "_id": "68d069e748439d63f889b03d",
                "name": "X-Burguer",
                "category": "MainCourse",
                "price": 15.9,
                "amount": 3
            }
        ],
        "status": "Done",
        "payment": {
            "method": "Pix",
            "status": "Authorized"
        },
        "totalPrice": 47.7
    },
    {
        "_id": ObjectId(),
        "customerId": "",
        "customerName": "",
        "items": [
            {
                "_id": "68d069e748439d63f889b03d",
                "name": "X-Burguer",
                "category": "MainCourse",
                "price": 15.9,
                "amount": 3
            }
        ],
        "status": "Finished",
        "payment": {
            "method": "Pix",
            "status": "Authorized"
        },
        "totalPrice": 47.7
    },
    {
        "_id": ObjectId(),
        "customerId": "",
        "customerName": "",
        "items": [
            {
                "_id": "68d069e748439d63f889b03d",
                "name": "X-Burguer",
                "category": "MainCourse",
                "price": 15.9,
                "amount": 3
            }
        ],
        "status": "Canceled",
        "payment": {
            "method": "Pix",
            "status": "Refused"
        },
        "totalPrice": 47.7
    }
]);
