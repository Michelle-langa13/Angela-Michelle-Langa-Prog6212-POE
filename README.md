# V7 Clean Build Fixed

FIXED: CS0006 Metadata file error.

Cause: previous zip included bin/obj folders. This zip has NO bin/obj - clean build.

## Build Steps (IMPORTANT - Do this first)

1. Extract zip to e.g. C:\Projects\RaceDay_V7
2. Open EventManagementAPI.sln in Visual Studio
3. Build > Clean Solution
4. Build > Rebuild Solution
5. Test > Run All Tests - should show 5 passed ✅
6. Run:
   - Set EventManagementAPI as startup -> F5 (https://localhost:7001/swagger)
   - Set EventManagementMVC as startup -> F5 (https://localhost:7002)

If you still see error:
- Close VS
- Delete all bin/obj folders manually
- Reopen .sln
- Rebuild

## Docker

docker-compose up --build
API http://localhost:7001/swagger
MVC http://localhost:7002

## Features

- API + Tests + MVC + Docker
- Cute Bow Theme + Real Pictures
- Login/Signup, Grid/List view, Filter, Create, AI
