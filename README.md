# MasonVirusV2

MasonVirusV2 is a C# application that demonstrates the use of low-level Windows API calls to manipulate the display and write data to the Master Boot Record (MBR) of the system. This code showcases techniques for screen drawing and system interactions.

## Features

- **Screen Manipulation**: The program continuously alters the display by drawing random shapes and text on the desktop.
- **MBR Modification**: It writes a predefined byte array to the Master Boot Record of the system drive.

## Code Explanation

### Key Components

1. **Using Windows API**:
   - The code imports functions from `kernel32.dll` and `user32.dll` to interact with the Windows operating system at a low level.
   - Functions like `CreateFile`, `WriteFile`, `GetDC`, and `BitBlt` are used to manipulate files and the display.

2. **Screen Size Calculation**:
   - The screen dimensions are retrieved using `GetSystemMetrics`, allowing the program to adapt its drawing to different screen sizes.

3. **Random Drawing**:
   - The program generates random shapes and positions on the screen, creating a visual effect.
   - A string, "FREEMASONRY", is drawn at the center of the screen using a bold font in red color.

4. **MBR Writing**:
   - The application attempts to write a byte array to the MBR of the physical drive, which can potentially corrupt the system if misused.

### Main Loop

The `Main` method contains an infinite loop that performs the following actions:
- Retrieves the device context for the desktop.
- Draws random colored rectangles on the screen.
- Moves the cursor to random positions.
- Writes data to the MBR.

## Usage

**Important**: This code is intended for educational purposes only. Running it on your system can result in data loss, system corruption, or other unintended consequences. Use at your own risk.

To compile and run this program, ensure you have a C# development environment set up, such as Visual Studio.

## Disclaimer

This software is provided "as is", without any express or implied warranty. The author is not responsible for any damage caused by the use or misuse of this code. It is the user's responsibility to ensure they are not violating any laws or regulations by running this code.

By using this code, you acknowledge that you understand the risks involved and agree not to hold the author liable for any issues that may arise.
