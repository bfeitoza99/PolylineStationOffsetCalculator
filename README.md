## Notes

- The `docker-compose.yml` automatically maps the files `points.xlsx` and `polyline.xlsx` for shared access between the host and container. It's very important don't move or change the file's name.
- If you encounter issues with port conflicts, ensure that ports **3000** (frontend) and **8001** (backend) are free before running the project.

---

Feel free to contribute or raise issues in the repository. 😊


# Polyline Station Offset Calculator

This project calculates the offset and station of given points relative to a polyline using a backend built with .NET 8 and a frontend built with Angular.

## How to Run the Project

### Method 1: Using Docker

#### Prerequisites
- Ensure **Docker** is installed and running on your machine.

#### Steps
1. Navigate to the root directory of the project where the `docker-compose.yml` file is located.
2. Run the following command in your terminal: docker-compose up -d

3. After the services are up, access the application:
- **Frontend**: [http://localhost:3000](http://localhost:3000)
- **Backend API**: [http://localhost:8001](http://localhost:8001)

### Method 2: Running Manually

#### Frontend
1. Ensure you have **node:18.19** installed on your machine.
1. Navigate to the `frontend` directory:
cd frontend

2. Install the dependencies:
npm install

3. Start the frontend server:
npm run start

4. The frontend will be available at:
- **URL**: [http://localhost:4200](http://localhost:4200)

#### Backend
1. Ensure you have **.NET SDK 8** installed on your machine.

2. Navigate to the `backend` directory:
cd backend/PolylineMinimal

3. Start the API:
dotnet run

4. The backend API will be available at:
- **URL**: [http://localhost:8001](http://localhost:8001)

## Requirements

### Docker
- Docker version compatible with `docker-compose`.

### Frontend
- **Node.js version**: 18.19.0
- **npm**: Installed with Node.js

### Backend
- **.NET SDK**: Version 8.0 or later

## Project Structure

polyline-station-offset-calculator/
│
├── backend/            # Backend API built with .NET 8
├── frontend/           # Frontend built with Angular
├── points.xlsx         # Input file for points
├── polyline.xlsx       # Input file for polyline
├── docker-compose.yml  # Docker configuration file
└── README.md           # Documentation

