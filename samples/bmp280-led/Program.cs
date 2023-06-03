using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Device.I2c;
using System.Threading.Tasks;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.ReadResult;

bool _fanOn = false;
int _pin = 21;

// Initialize the GPIO controller
using GpioController gpio = new GpioController();

// Open the GPIO pin for output
gpio.OpenPin(_pin, PinMode.Output);
gpio.Write(_pin, PinValue.Low);

// Get a reference to a device on the I2C bus
// var i2cSettings = 
var i2cSettings = new I2cConnectionSettings(1, Bme280.SecondaryI2cAddress); // new I2cConnectionSettings(1, Bme280.DefaultI2cAddress)
using I2cDevice i2cDevice = I2cDevice.Create(i2cSettings);

// Create a reference to the BME280
using var bme280 = new Bme280(i2cDevice);


while (true)
{
    // Read the BME280
    Bme280ReadResult output = bme280.Read();
    double temperatureF = output.Temperature?.DegreesCelsius ?? double.NaN;
    double humidityPercent = output.Humidity?.Percent ?? double.NaN;

    // Print statuses
    Console.WriteLine();
    Console.WriteLine("DEVICE STATUS");
    Console.WriteLine("-------------");
    Console.WriteLine($"Fan: {(_fanOn ? "ON" : "OFF")}");
    Console.WriteLine($"Temperature: {temperatureF:0.#}°F");
    Console.WriteLine($"Relative humidity: {humidityPercent:#.##}%");
    Console.WriteLine();
    Console.WriteLine("Enter command (status/fan/exit):");

    if (humidityPercent > 60)
    { gpio.Write(_pin, PinValue.High); }
    else
    { gpio.Write(_pin, PinValue.Low); }
    await Task.Delay(1000);
}

// Close the pin before exit
// gpio.ClosePin(_pin)
