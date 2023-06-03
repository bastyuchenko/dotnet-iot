https://learn.microsoft.com/en-us/training/modules/create-iot-device-dotnet/


ssh abastiuchenko@192.168.1.128

cd ..
rm -rf bmp280-led
mkdir bmp280-led
cd bmp280-led


dotnet publish --runtime linux-arm --self-contained


scp ./bin/Debug/net6.0/linux-arm/publish/* abastiuchenko@192.168.1.128:~/bmp280-led


chmod +x ./bmp280-led
