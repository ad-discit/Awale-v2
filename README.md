# Awale v2
This project aims to implement the traditional African game of Awalé (also known as Oware or Wure in Wolof) using Object-Oriented Programming (OOP) principles in C#

Awalé is a strategy board game popular across West and Southern Africa, known for its rich cultural heritage and profound strategic depth. The game involves two players, each managing seeds across twelve pits (six per side) with the goal of capturing more seeds than the opponent.

# Features
Game Logic: Implements the traditional rules of Awalé, handling seed sowing, capturing, and game-ending conditions.
AI Opponent: An AI opponent for single-player mode, with varying levels of difficulty.
Multiplayer: Local (same device) multiplayer support.
GUI: A graphical user interface (GUI) that visually represents the game board and allows for intuitive interaction.
Cultural Insight: Includes background information on the cultural significance of Awalé in African societies.

# Goals
To provide an interactive and engaging way to experience playing Awalé.
To apply and demonstrate OOP principles and design patterns in a real-world project.
To preserve and share an important aspect of African cultural heritage.

# Implementing a Graphical Window for Awalé Game in C#
**Tools and Technologies**
.NET Framework: A software framework developed by Microsoft that provides a large library and language interoperability across several programming languages. .NET Framework includes Windows Forms, a GUI library for creating desktop applications.
Windows Forms (WinForms): A GUI class library included in the Microsoft .NET Framework, providing a platform for developing rich desktop applications. Ideal for creating the graphical window for the Awalé game.
Visual Studio: An integrated development environment (IDE) from Microsoft. It provides tools for designing, developing, and testing Windows Forms applications.
Steps to Implement the Graphic Window

# Designing the Game Window Step 1
Design the GUI: Use the Visual Studio Designer to design the game window. Drag and drop controls like Panels, Buttons, or PictureBoxes from the Toolbox to represent the game board and pits. Use Labels for displaying scores and game status messages.
Customize Controls: Customize the appearance and properties of controls to fit the aesthetic of Awalé. You can set properties like background color, size, and font in the Properties window.

# Implementing Game Logic Step 2
Game Board Representation: Implement a class to represent the game board logic, including the distribution of seeds, capturing rules, and determining the game's end.
Event Handling: Write event handlers for user interactions (e.g., clicking on a pit). These handlers will update the game state and the GUI accordingly.
Updating the GUI: After each move, update the GUI to reflect the current state of the game board. This includes updating the number of seeds in each pit and the scores.

# Running and Testing Step 3
Run the Application: Use Visual Studio's built-in debugger to run your application. Test the functionality of your game thoroughly to ensure that all interactions work as expected.
Debugging: Use Visual Studio's debugging tools to identify and fix any issues. Pay special attention to game logic and GUI updates.

# Packaging and Distribution Step 4
Build the Application: Once testing is complete and application is ready create an executable (.exe) file.
Distribution: can now distribute the executable file to others, allowing them to install and play the Awalé game on their Windows devices.
