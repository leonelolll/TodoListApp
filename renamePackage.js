const fs = require('fs');
const path = require('path');

const oldPackageName = 'com.myappblankbare';
const newPackageName = 'com.todolistapp';

const filesToUpdate = [
  'android/app/src/main/AndroidManifest.xml',
  'android/app/build.gradle',
  'android/app/src/main/java/com/todolistapp/MainActivity.kt',
  'android/app/src/main/java/com/todolistapp/MainApplication.kt'
  // Add more file paths as needed
];

const updateFileContent = (filePath, oldContent, newContent) => {
  if (fs.existsSync(filePath)) {
    const fileContent = fs.readFileSync(filePath, 'utf8');
    const updatedContent = fileContent.replace(new RegExp(oldContent, 'g'), newContent);
    fs.writeFileSync(filePath, updatedContent, 'utf8');
    console.log(`Updated content in ${filePath}`);
  }
};

filesToUpdate.forEach((filePath) => {
  updateFileContent(filePath, oldPackageName, newPackageName);
});

const buildGradlePath = 'android/app/build.gradle';
if (fs.existsSync(buildGradlePath)) {
  updateFileContent(buildGradlePath, `applicationId "${oldPackageName}"`, `applicationId "${newPackageName}"`);
}

const buckFilePath = 'android/app/BUCK';
if (fs.existsSync(buckFilePath)) {
  updateFileContent(buckFilePath, `package = "${oldPackageName}"`, `package = "${newPackageName}"`);
}

const oldPackagePath = path.join('android/app/src/main/java', ...oldPackageName.split('.'));
const newPackagePath = path.join('android/app/src/main/java', ...newPackageName.split('.'));

const createDirectories = (dirPath) => {
  if (!fs.existsSync(dirPath)) {
    fs.mkdirSync(dirPath, { recursive: true });
    console.log(`Created directory ${dirPath}`);
  }
};

createDirectories(newPackagePath);

const moveFiles = (oldPath, newPath) => {
  if (fs.existsSync(oldPath)) {
    fs.readdirSync(oldPath).forEach(file => {
      const oldFilePath = path.join(oldPath, file);
      const newFilePath = path.join(newPath, file);
      fs.renameSync(oldFilePath, newFilePath);
      console.log(`Moved ${oldFilePath} to ${newFilePath}`);
    });
  }
};

moveFiles(oldPackagePath, newPackagePath);

const deleteDirectory = (dirPath) => {
  if (fs.existsSync(dirPath)) {
    fs.readdirSync(dirPath).forEach(file => {
      const currentPath = path.join(dirPath, file);
      if (fs.lstatSync(currentPath).isDirectory()) {
        deleteDirectory(currentPath);
      } else {
        fs.unlinkSync(currentPath);
      }
    });
    fs.rmdirSync(dirPath);
    console.log(`Deleted directory ${dirPath}`);
  }
};

deleteDirectory(oldPackagePath);
