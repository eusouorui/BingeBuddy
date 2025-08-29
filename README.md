## Commands to run different OS:

#### In the BingeBuddy.Maui directory run:

For windows:
```
dotnet build -t:Run -f net9.0-windows 
```
For MacOS:
``` 
dotnet build -t:Run -f net9.0-maccatalyst 
```
For iOS:
``` 
dotnet build -t:Run -f net9.0-ios 
```
For Android:
``` 
dotnet build -t:Run -f net9.0-android
```    

## COmmands to run web version:

#### In the BingeBuddy.Web directory run:
```
dotnet run
```
