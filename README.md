# partycli

A command-line tool for managing and querying NordVPN servers, built on .NET Framework 4.8.

## Features

- **List VPN Servers:** Fetch and display NordVPN server lists, with options to filter by country (e.g., France) and protocol (TCP).
- **Local Server Storage:** Save server lists locally and display them without re-fetching.
- **Configuration Management:** Set and retrieve configuration values via CLI.
- **Flexible Storage:** Choose between local app data or application settings for storing configuration and server lists.

# Commands:

## `server_list`
Manage VPN server lists.

**Options:**
- `--local` : Display locally saved server list.
- `--france` : Filter servers by France.
- `--tcp` : Filter servers supporting TCP protocol.
- `-?` or `-h` or `--help` : Show help information for this command.

**Example:**
- `partycli server_list --local`
## `config`
Manage configuration values.

Arguments:
- `<name>`   Configuration name
- `<value>`  Configuration value

**Options:**
- `-?` or `-h` or `--help` : Show help information for this command.

**Example:**
- `partycli config <name> <value>` 

---
## Storage

- By default, uses `ApplicationSettingsStorageService` for storing data in user settings.
- Optionally, switch to `AppDataStorageService` for storing data in `%APPDATA%\partycli`.

---
## Requirements

- .NET Framework 4.8
- Internet connection for fetching server lists
