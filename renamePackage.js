const { exec } = require('child_process');

const newName = process.argv[2];
const newBundleId = process.argv[3];

if (!newName || !newBundleId) {
  console.log('Usage: node renamePackage.js <NewAppName> <new.bundle.id>');
  process.exit(1);
}

exec(`react-native-rename "${newName}" -b ${newBundleId} --skipGitStatusCheck`, (error, stdout, stderr) => {
  if (error) {
    console.error(`Error: ${error.message}`);
    return;
  }
  if (stderr) {
    console.error(`Stderr: ${stderr}`);
    return;
  }
  console.log(`Stdout: ${stdout}`);
});
