
# To-Do List App

This is a simple To-Do List app developed using Expo and React Native. The app allows users to add tasks, view them in a list, and mark them as complete by tapping on them. This project serves as an introductory project for learning React Native and Expo.

## Features

- Add tasks to the list.
- View tasks in a list format.
- Mark tasks as complete by tapping on them.

## Getting Started

### Prerequisites

- Node.js (>= 14.0.0)
- npm (>= 6.0.0) or yarn
- Expo CLI

### Installation

1. Install Expo CLI globally if you haven't already:
   ```sh
   npm install -g expo-cli
   ```

2. Clone the repository:
   ```sh
   git clone https://github.com/leonelolll/TodoListApp.git
   cd todo-list-app
   ```

3. Install dependencies:
   ```sh
   npm install
   ```

4. Start the app:
   ```sh
   npm start
   ```
   or
   ```sh
   expo start
   ```

### Running on a Device/Emulator

- Follow the instructions provided by Expo to run the app on a physical device or an emulator. You can use the Expo Go app on Android or iOS to view your project.

## Project Structure

- **App.js**: The main app component where tasks are added and displayed.
- **components/Task.js**: A component that defines the appearance and behavior of individual tasks.

## Code Overview

### App.js

This is the main file that contains the core logic and UI for the app. It manages the state for the task input and the list of tasks.

### Task.js

This component is responsible for rendering individual tasks. It includes styles for the task appearance and structure.

## Styling

The app uses React Native's `StyleSheet` for styling components. Styles are defined at the bottom of the `App.js` and `Task.js` files.

## Tutorials

This app was developed by following the code from these YouTube tutorials:
- [Build your first React Native app - Todo List Tutorial Part 1](https://www.youtube.com/watch?v=0kL6nhutjQ8)
- [Build your first React Native app - Todo List Tutorial Part 2](https://www.youtube.com/watch?v=00HFzh3w1B8)

## Future Improvements

- Add functionality to edit tasks.
- Add persistent storage using AsyncStorage or a database.
- Implement task categories and priorities.
- Improve UI/UX with animations and better styling.
  
## Acknowledgements

- This app was developed as a learning project using the Expo and React Native frameworks.
- Inspired by various React Native tutorials and documentation.

