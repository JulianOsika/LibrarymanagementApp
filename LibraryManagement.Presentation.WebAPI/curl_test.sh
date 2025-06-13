#!/bin/bash

echo "GET all readers"
curl -k -X GET https://localhost:7007/api/reader
echo -e "\n"

echo "POST new reader"
curl -k -X POST https://localhost:7007/api/reader \
  -H "Content-Type: application/json" \
  -d '{"name":"Jan","surname":"Kowalski","birthDate":"1990-01-01"}'
echo -e "\n"

echo "GET reader by ID"
curl -k -X GET https://localhost:7007/api/reader/1
echo -e "\n"

echo "PUT update reader"
curl -k -X PUT https://localhost:7007/api/reader/1 \
  -H "Content-Type: application/json" \
  -d '{"name":"Zmieniony","surname":"Czytelnik","birthDate":"1990-01-01"}'
echo -e "\n"

echo "DELETE reader by ID"
curl -k -X DELETE https://localhost:7007/api/reader/1
echo -e "\n"



echo "GET all books"
curl -k -X GET https://localhost:7007/api/book
echo -e "\n"

echo "POST new book"
curl -k -X POST https://localhost:7007/api/book \
  -H "Content-Type: application/json" \
  -d '{"title":"Czysty kod","description":"Klasyk o czystym kodzie","authorName":"Robert","authorSurname":"Martin"}'
echo -e "\n"


echo "GET book by ID"
curl -k -X GET https://localhost:7007/api/book/1
echo -e "\n"

echo "PUT update book"
curl -k -X PUT https://localhost:7007/api/book/1 \
  -H "Content-Type: application/json" \
  -d '{"title":"Zmieniony tytu³","description":"Opis zaktualizowany","authorName":"Zmieniony","authorSurname":"Autor"}'
echo -e "\n"


echo "DELETE book by ID"
curl -k -X DELETE https://localhost:7007/api/book/1
echo -e "\n"


echo "GET all loans"
curl -k -X GET https://localhost:7007/api/loan
echo -e "\n"

echo "POST new loan"
curl -k -X POST https://localhost:7007/api/loan \
  -H "Content-Type: application/json" \
  -d '{"readerId":2,"bookId":3,"loanDate":"2024-05-28"}'
echo -e "\n"

echo "GET loan by ID"
curl -k -X GET https://localhost:7007/api/loan/1
echo -e "\n"

echo "PUT update loan"
curl -k -X PUT https://localhost:7007/api/loan/1 \
  -H "Content-Type: application/json" \
  -d '{"readerId":2,"bookId":3,"loanDate":"2024-05-28","returnDate":"2024-06-10"}'
echo -e "\n"

echo "DELETE loan by ID"
curl -k -X DELETE https://localhost:7007/api/loan/1
echo -e "\n"

