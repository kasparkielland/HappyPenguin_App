Happy Penguin Mobile Applications Code

The following Tree-structure are this repository's structure where the different directories are for AndroidStudio App, Xamarin Cross-Platform App and Xcode App (for running Xamarin App on iOS)
'''
.
├── HappyPenguinAndroid
│   └── HappyPenguin_new
│       ├── app
│       │   ├── build.gradle
│       │   ├── proguard-rules.pro
│       │   └── src
│       │       ├── androidTest
│       │       │   └── java
│       │       │       └── com
│       │       │           └── example
│       │       │               └── happypenguin
│       │       │                   └── ExampleInstrumentedTest.java
│       │       ├── main
│       │       │   ├── AndroidManifest.xml
│       │       │   ├── java
│       │       │   │   └── com
│       │       │   │       └── example
│       │       │   │           └── happypenguin
│       │       │   │               ├── BlueContext.java
│       │       │   │               ├── DataContext.java
│       │       │   │               ├── GraphActivity.java
│       │       │   │               ├── MainActivity.java
│       │       │   │               └── MqttDetails.java
│       │       │   └── res
│       │       │       ├── drawable
│       │       │       │   └── ic_launcher_background.xml
│       │       │       ├── drawable-v24
│       │       │       │   └── ic_launcher_foreground.xml
│       │       │       ├── layout
│       │       │       │   ├── activity_graph.xml
│       │       │       │   ├── activity_main.xml
│       │       │       │   └── activity_mqtt_details.xml
│       │       │       ├── mipmap-anydpi-v26
│       │       │       │   ├── ic_launcher.xml
│       │       │       │   └── ic_launcher_round.xml
│       │       │       ├── mipmap-hdpi
│       │       │       │   ├── ic_launcher.png
│       │       │       │   └── ic_launcher_round.png
│       │       │       ├── mipmap-mdpi
│       │       │       │   ├── ic_launcher.png
│       │       │       │   └── ic_launcher_round.png
│       │       │       ├── mipmap-xhdpi
│       │       │       │   ├── ic_launcher.png
│       │       │       │   └── ic_launcher_round.png
│       │       │       ├── mipmap-xxhdpi
│       │       │       │   ├── ic_launcher.png
│       │       │       │   └── ic_launcher_round.png
│       │       │       ├── mipmap-xxxhdpi
│       │       │       │   ├── ic_launcher.png
│       │       │       │   └── ic_launcher_round.png
│       │       │       └── values
│       │       │           ├── colors.xml
│       │       │           ├── strings.xml
│       │       │           └── styles.xml
│       │       └── test
│       │           └── java
│       │               └── com
│       │                   └── example
│       │                       └── happypenguin
│       │                           └── ExampleUnitTest.java
│       ├── build.gradle
│       ├── gradle
│       │   └── wrapper
│       │       ├── gradle-wrapper.jar
│       │       └── gradle-wrapper.properties
│       ├── gradle.properties
│       ├── gradlew
│       ├── gradlew.bat
│       └── settings.gradle
├── HappyPenguinXamarin
│   ├── HappyPenguin
│   │   ├── App.xaml
│   │   ├── App.xaml.cs
│   │   ├── AppShell.xaml
│   │   ├── AppShell.xaml.cs
│   │   ├── AssemblyInfo.cs
│   │   ├── Controls
│   │   │   ├── CircleScanner.xaml
│   │   │   └── CircleScanner.xaml.cs
│   │   ├── HappyPenguin.csproj
│   │   ├── Models
│   │   │   ├── DeviceModel.cs
│   │   │   └── NativeDevice.cs
│   │   ├── Utils
│   │   │   └── FontAwsomeIcons.cs
│   │   ├── ViewModels
│   │   │   ├── BluetoothViewModel.cs
│   │   │   └── WeightViewModel.cs
│   │   └── Views
│   │       ├── AboutPage.xaml
│   │       ├── AboutPage.xaml.cs
│   │       ├── BluetoothPage.xaml
│   │       ├── BluetoothPage.xaml.cs
│   │       ├── DashboardPage.xaml
│   │       ├── DashboardPage.xaml.cs
│   │       ├── GraphPage.xaml
│   │       ├── GraphPage.xaml.cs
│   │       ├── HelpPage.xaml
│   │       ├── HelpPage.xaml.cs
│   │       ├── SettingsPage.xaml
│   │       ├── SettingsPage.xaml.cs
│   │       ├── WeightPage.xaml
│   │       └── WeightPage.xaml.cs
│   ├── HappyPenguin.Android
│   │   ├── Assets
│   │   │   ├── AboutAssets.txt
│   │   │   ├── FontAwesomeBrands.ttf
│   │   │   ├── FontAwesomeRegular.ttf
│   │   │   └── FontAwesomeSolid.ttf
│   │   ├── HappyPenguin.Android.csproj
│   │   ├── MainActivity.cs
│   │   ├── Properties
│   │   │   ├── AndroidManifest.xml
│   │   │   └── AssemblyInfo.cs
│   │   └── Resources
│   │       ├── AboutResources.txt
│   │       ├── Resource.designer.cs
│   │       ├── layout
│   │       │   ├── Tabbar.xml
│   │       │   └── Toolbar.xml
│   │       ├── mipmap-anydpi-v26
│   │       │   ├── icon.xml
│   │       │   └── icon_round.xml
│   │       ├── mipmap-hdpi
│   │       │   ├── icon.png
│   │       │   └── launcher_foreground.png
│   │       ├── mipmap-mdpi
│   │       │   ├── icon.png
│   │       │   └── launcher_foreground.png
│   │       ├── mipmap-xhdpi
│   │       │   ├── icon.png
│   │       │   └── launcher_foreground.png
│   │       ├── mipmap-xxhdpi
│   │       │   ├── icon.png
│   │       │   └── launcher_foreground.png
│   │       ├── mipmap-xxxhdpi
│   │       │   ├── icon.png
│   │       │   └── launcher_foreground.png
│   │       └── values
│   │           ├── colors.xml
│   │           └── styles.xml
│   ├── HappyPenguin.iOS
│   │   ├── AppDelegate.cs
│   │   ├── Assets.xcassets
│   │   │   └── AppIcon.appiconset
│   │   │       ├── Contents.json
│   │   │       ├── HappyPenguin_Logo-120x120.png
│   │   │       └── HappyPenguin_Logo-180x180.png
│   │   ├── Entitlements.plist
│   │   ├── HappyPenguin.iOS.csproj
│   │   ├── Info.plist
│   │   ├── Main.cs
│   │   ├── Properties
│   │   │   └── AssemblyInfo.cs
│   │   └── Resources
│   │       ├── Default-568h@2x.png
│   │       ├── Default-Portrait.png
│   │       ├── Default-Portrait@2x.png
│   │       ├── Default.png
│   │       ├── Default@2x.png
│   │       ├── FontAwesomeBrands.ttf
│   │       ├── FontAwesomeRegular.ttf
│   │       ├── FontAwesomeSolid.ttf
│   │       └── LaunchScreen.storyboard
│   ├── HappyPenguin.sln
│   └── README.md
└── HappyPenguiniOS
    ├── HappyPenguiniOS
    │   ├── AppDelegate.swift
    │   ├── Assets.xcassets
    │   │   ├── AppIcon.appiconset
    │   │   │   └── Contents.json
    │   │   └── Contents.json
    │   ├── Base.lproj
    │   │   └── LaunchScreen.storyboard
    │   ├── ContentView.swift
    │   ├── Info.plist
    │   ├── Preview\ Content
    │   │   └── Preview\ Assets.xcassets
    │   │       └── Contents.json
    │   └── SceneDelegate.swift
    ├── HappyPenguiniOS.xcodeproj
    │   ├── project.pbxproj
    │   ├── project.xcworkspace
    │   │   ├── contents.xcworkspacedata
    │   │   ├── xcshareddata
    │   │   │   └── IDEWorkspaceChecks.plist
    │   │   └── xcuserdata
    │   │       └── kaspar.xcuserdatad
    │   │           └── UserInterfaceState.xcuserstate
    │   └── xcuserdata
    │       └── kaspar.xcuserdatad
    │           └── xcschemes
    │               └── xcschememanagement.plist
    ├── HappyPenguiniOSTests
    │   ├── HappyPenguiniOSTests.swift
    │   └── Info.plist
    └── HappyPenguiniOSUITests
        ├── HappyPenguiniOSUITests.swift
        └── Info.plist
'''
