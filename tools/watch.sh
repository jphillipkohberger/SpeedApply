    #!/bin/bash

    WATCH_DIR="/app"
    SCRIPT_TO_BUILD="/app/tools/build.sh"
    SCRIPT_TO_RUN="/app/tools/run.sh"

    inotifywait -m -r -e close_write "$WATCH_DIR" | while read -r directory event file; do
        echo "File saved: $directory$file"
        echo "Building SpeedApply Project"
        "$SCRIPT_TO_BUILD" "$directory$file" # Pass the saved file path to your script
        echo "Running SpeedApply Project"
        "$SCRIPT_TO_RUN" "$directory$file" # Pass the saved file path to your script
    done