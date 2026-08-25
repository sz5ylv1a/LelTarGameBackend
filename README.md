# Lel.tar Backend

Backend server for the school project game **[Lel.tar](https://github.com/LohinSys/LelTarGame)**, made using **ASP.NET 10.0** and **Visual Studio 2026**.

## Role Index

|	| Role name	| Description |
|---| --------- | ----------- |
|👑	| Admin		| All priviliges |
|🔨	| Moderator	| Priviliges to manage users |
|👤	| User		| Default role given to all users |
|❌	| Banned	| Banned users who have no priviliges whatsoever |

## API requests

All API requests are handled at `https://leltargame.tryasp.net/api`, and must be used accordingly. The requested data for anything that isn't a GET request must be a valid JSON string due to how it's designed.

### v2.0 *(latest, recommended)*

#### Account Management
- `api/v2/accMgmt/view/all` - View all registered users
- `api/v2/accMgmt/view/#` - View a specific user by their unique ID
- `api/v2/accMgmt/#/updateUsername` - Update target user's username
- `api/v2/accMgmt/#/updateEmail` - Update target user's e-mail address
- `api/v2/accMgmt/#/updatePassword` - Update target user's password
- `api/v2/accMgmt/#/updateCountry` - Update target user's assigned country of origin *(specify country ID `0` for none)*
- `api/v2/accMgmt/#/updateRole` - Update the role of the target user (Admin only, however Moderators can use this to ban people)
- `api/v2/accMgmt/#/deleteAccount` - Delete the target user's account, including all associated data with it

#### Authorization
- `api/v2/auth/register` - Registers an account
- `api/v2/auth/login` - Login with existing account

#### Dummy Data
- `api/v2/dummy/countries` - View all listed countries, including IDs and flags
- `api/v2/dummy/countries/#` - View a specific country by its ID *(0-205)*
- `api/v2/dummy/difficulties` - View all listed difficulties, including IDs and icons
- `api/v2/dummy/difficulties/#` - View a specific difficulty by its ID *(1-4)*

#### Leaderboards
- `api/v2/lbs/all` - View all leaderboard entries
- `api/v2/lbs/#` - View only a specific leaderboard entry
- `api/v2/lbs/submit` - Submit score to the leaderboards
- `api/v2/lbs/#/disqualify` - Disqualify a leaderboard entry, preventing it from being shown in the public leaderboards (Admin and Moderator only)

### v1.0 *(deprecated, read-only)*

- Documenting soon... or not...