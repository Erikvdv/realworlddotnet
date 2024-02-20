To create a self-signed rsa certificate using openssl use the following commands
openssl genpkey -algorithm RSA -out private_key.pem
openssl req -new -x509 -key private_key.pem -out cert.pem -days 365
openssl pkcs12 -export -out certificate.pfx -inkey private_key.pem -in cert.pem
