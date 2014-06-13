~~~
// Actual Work Time Periods and Immediate Tasks
~~~

# Version 1

## Period 1

### 2014-06-11 5:57-5:58 Plan

What is the most important task to complete in this hour?

- Create the scrolling background of BushRun to ensure a complete project is possible

### 5:59-6:31

- Add background
- Flip y coordinates to be the same as a standard x-y plane (-1,-1) is bottom left

### 6:32-7:15

- Tile background


## Period 2

### 2014-06-12 5:37-5:38 Plan

I will handle user input

### 5:39-7:43

- Add input provider
- Implement WPF Input Provider

## Period 3

### 9:35-9:36 Plan

I will implement the input engine to process the input for the game.

### 9:37-10:02

- Add InputEngine
- Move Entity Definitions and generation to separate project

### 10:03-10:10
### 10:14-11:49
### 11:53-12:30
### 12:50-13:00
### 13:25-13:39

- Implement InputEngine
- Add Generics support to Entity Generator
- Add Inputable (Handles Global Input from InputProvider)
	- Callback Handler
	- Input Callback Parameters:
		- Type
			- Press (Mouse, Tap)
			- Keyboard
		- Values(If keyboard - supports multi-key, multi-touch)
			- HasChanged (False if still holding, but a separate input value changed)
			- Down, Hold, Up
			- Value
			- Position

## Period 4

### 2014-06-13 5:38-5:40 Plan

What is most important to accomplish this hour?

I will finish the scrolling backgrounds to support parallax (scrolling at different rates) controlled by a scrolling controller.

### 5:41-

- Create Scrollable, Scrolling, and ScrollEngine



# FUTURE

## GameCore

- Design dependencies on other behaviors 
	- (Drawable Depends on Placeable)
	- (Scrollable Depends on Placeable)

- Extend InputEngine
	- Priority
	- Do GameBounds testing
	- Handle KeyUp occuring outside of app

- Convert Screen Coordinates to use ints
- Add SpineProvider
- Add SoundProvider

## Bush Run
- Create Scrolling Backgrounds
- Add Player Image
- Add Lion Image
- Add Snake Obstacle
- Change Player to a spine animation
- Add sounds
