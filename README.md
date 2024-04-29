# JSonAutoPorting

This project stems from the necessity of having a useful tool to align various translation files across different clients within a project.

## Table of contents

- [Install](#install)
- [Settings](#settings)
- [Contribute](#contribute)
- [Licence](#licence)


### Install

1. Download the project
2. Configure your [settings](#settings)
3. Build the project
4. Run App.exe in the debug folder of the App project
   
### Settings

```json
{
  "rootDirectory": "C:\\wherearethefiletarget",
  "fileSource": "it.json",
  "fileTarget": "it.json",
  "sourceFilePath": "C:\\whereismysourcefile",
  "subDirectoryToSeachFileTarget": "folder1\\folder2",
  "includeTargetDifferentKeys": true
}
```

- **rootDirectory**: contains the folder where you wanna to start the search and porting of the files
- **fileSource**: the name of the soure file where copy the keys to target
- **fileTarget**: the name of all target files where we wanna to copy the keys of the file source
- **subDirectoryToSeachFileTarget**: from rootDirectory, I wanna to find the file target only in this subdirectory path
- **includeTargetDifferentKeys**: if we wanna include in the result of the compare the keys there are only in the target file

### Contribute

1. Project fork
2. Clone the fork
3. Create a branch for your edit (`git checkout -b feature/edit-name`)
4. Commit your changes (`git commit -am 'Adding a new feature'`)
5. Push to branch (`git push origin feature/edit-name`)
6. Create a new pull request

### Licence

This project is distributed under the License [MIT License](https://opensource.org/licenses/MIT)
