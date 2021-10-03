# kafka-tests

## Setup
To run this sample, exec the following commands:
- `docker-compose up`
- `docker-compose exec kafka-tests bash` in another window to connect to kafka via bash
- `kafka-topics --create --topic orders-example --bootstrap-server kafka:9092 --replication-factor 1 --partitions 3` to create the topic

## Publishing OrderEvent
Run the webapi and post an OrderEvent on the route `POST/Order`

OrderEvent:

```
{
    "orderId": "d70d2f9ae7a846ee9b25f331fce0fb2e",
    "orderDate": "2021-10-03T20:25:23.696Z",
    "buyerId": "d6bf5bb531624e7f91679e01ae8f1eb3",
    "orderStatus": 0,
    "description": "we kafka learning",
    "address": {
        "street": "ag engenheiro",
        "city": "indaiatuba",
        "state": "SP",
        "country": "Brazil",
        "zipcode": "13345705"
    },
    "orderItems": [
        {
            "productName": "whey protein",
            "pictureUrl": "https://google.com",
            "unitPrice": 100.90,
            "discount": 0,
            "units": 2
        },
        {
            "productName": "whey protein concentrate",
            "pictureUrl": "https://google.com",
            "unitPrice": 80.90,
            "discount": 0,
            "units": 2
        },
        {
            "productName": "protein bar",
            "pictureUrl": "https://google.com",
            "unitPrice": 12.90,
            "discount": 5,
            "units": 10
        }
    ]
}
```