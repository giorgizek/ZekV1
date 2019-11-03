set OPENSSL_CONF=c:\OpenSSL-Win64\bin\openssl.cfg

C:\OpenSSL-Win64\openssl genrsa -out private.pem 2048
C:\OpenSSL-Win64\openssl req -new -x509 -key private.pem -out public.pem -days 1095
C:\OpenSSL-Win64\openssl pkcs12 -export -out cert.pfx -inkey private.pem -in public.pem

pause