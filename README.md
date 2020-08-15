# Schedule Manager

This is a desktop application I made to help with polling projects I am working on.

## Installation

```
Download & Install the msi file at 
https://files.goveratech.com/SetupScheduleManager.msi
```

### Prelaunch
- Create database titled "schedule_manager" (mysql database only)

### Post launch
#### Pre Task Save
- on application launch, set the default timer interval which you want (seconds,minutes or hours)
- add tasks by clicking on add task
  + Task Title
  + Set Custom time for this task (i.e this task has its own duration different from the default)
  + In Between (this means that every second interval, this task is to be activated)
  + Remove: to remove this task before it's saved to the db
- Save tasks
#### Post Task Save
-  Activate Button: Set the current task as the active task/override the current active task
- Turn Off Button: turn of this task 
#### Features
- Timer Labels: Show information pertaining to timer start and end duration.
- Active Task Label: Shows the active task
- Message Label: Shows app messages to the user
- Duration Label: Shows the default/custom duration to the user 
- View History Button: Displays a pop up dialog form showing the start and end timer duration for this instance run
- Start Button: Starts the interval
- End Button: Ends the interval
- Pause/Resume Button: 
  + on pause get time difference between now and due time for current interval 
  + on resume set timer interval to end based on calculated difference

# Schedule Manager