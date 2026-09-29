# SaintsProject #

[![unity_version](https://github.com/user-attachments/assets/dffbf530-6212-481b-bfdb-1e9d9ce3712d)](https://unity.com/download)
[![openupm](https://img.shields.io/npm/v/today.comes.saintsproject?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/today.comes.saintsproject/)
[![openupm](https://img.shields.io/badge/dynamic/json?color=brightgreen&label=downloads&query=%24.downloads&suffix=%2Fmonth&url=https%3A%2F%2Fpackage.openupm.com%2Fdownloads%2Fpoint%2Flast-month%2Ftoday.comes.saintsproject)](https://openupm.com/packages/today.comes.saintsproject/)

Unity Project Tab enhancement. Use `Alt`+`Left Mouse Button` to select.

![](https://github.com/user-attachments/assets/a1a4916b-b679-4525-8701-7e283a2a8448)
![](https://github.com/user-attachments/assets/9ec7a0a2-7793-4593-9026-bcddf0c8edd9)
![](https://github.com/user-attachments/assets/92d90e59-8927-48fe-9f16-87a78d13c857)
![](https://github.com/user-attachments/assets/6677009c-0af2-42f1-9001-9a34dcc7352c)


## Features ##

1.  Indent Guild lines
2.  Background Strip
3.  Minimal Mode
4.  AUto Icons
5.  Content Minimap
6.  Favorite Folders/Assets
7.  Split for personal/team-shared configs
8.  Custom icon & color
9.  Support Unity 6000+ / Disabled Domain Reload features

## Usage ##

1.  alt-click to change color and icon.
2.  icon field can search built-in icons. But you can still input an image asset path for custom icons.
3.  Tools - Saints Project to change different settings.


### Installation ###

*   Using git upm (Unity UI):

    1. `Window` - `Package Manager`
    2. Click `+` button, `Add package from git URL`
    3. Enter the following URL:

    ```
    https://github.com/TylerTemp/SaintsProject.git
    ```

*   Using git upm:

    add to `Packages/manifest.json` in your project

    ```javascript
    {
        "dependencies": {
            "today.comes.saintsproject": "https://github.com/TylerTemp/SaintsProject.git",
            // your other dependencies...
        }
    }
    ```

*   Using [OpenUPM](https://openupm.com/packages/today.comes.saintsfield/)

    ```bash
    openupm add today.comes.saintsproject
    ```

*   Using a git submodule:

    ```bash
    git submodule add https://github.com/TylerTemp/SaintsProject.git Packages/today.comes.saintsproject
    ```

    Note: submodule will not auto upgrade. please read [Git Submodule](https://git-scm.com/book/en/v2/Git-Tools-Submodules) to know how to upgrade

*   Using a `unitypackage` (NOT RECOMMENDED):

    Go to the [Release Page](https://github.com/TylerTemp/SaintsProject/releases) to download a desired version of `unitypackage` and import it to your project

