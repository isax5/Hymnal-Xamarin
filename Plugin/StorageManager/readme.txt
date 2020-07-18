------------------------
    Storage Manager
------------------------

1. Android
Use Realm NuGet
Call from MainActivity CrossStorageManager.Current.Init(Realms.Realm.GetInstance());

2. iOS
Use Realm NuGet
Call from AppDelegate CrossStorageManager.Current.Init(Realms.Realm.GetInstance());

3. Other Platforms
Use Xamarin.Essentials
Call CrossStorageManager.Current.Init();


Isaac Rebolledo