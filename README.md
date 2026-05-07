# Blazor Server DataGrid — URL Adaptor with CRUD Operations

This example shows that how to bind local data and perform CRUD operations at server by using URLAdaptor.

In this demo, [Blazor DataGrid](https://www.syncfusion.com/blazor-components/blazor-datagrid) is bound using URLAdaptor. CRUD operation along with data operations like filtering, sorting will be performed in server side. We have used API Contoller's post method to handle multiple post request for CRUD opertion.

## Features

- **Server-side data operations**: Sorting, Filtering, Paging, Counts
- **CRUD operations**: Add, Edit, Delete using toolbar actions
- **Integation**: URL Adaptor integration via `SfDataManager`

## Prerequisites

* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or later
* [Visual Studio Code](https://code.visualstudio.com/)
* [.NET SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)

## Getting Started

### Clone the repository

```bash
git clone https://github.com/SyncfusionExamples/Blazor-DataGrid-UrlAdaptor-CRUD-SortFilter.git
cd URLAdaptorSample
```

### Run with Visual Studio

1. Open the solution file using Visual Studio 2022 or later.
2. Restore the NuGet packages by rebuilding the solution.
3. Build the project to ensure there are no compilation errors.
4. Run the project.

### Run with .NET CLI

```bash
# Restore dependencies
dotnet restore

# Run the project
dotnet run
```

## References

**Documentation**: https://blazor.syncfusion.com/documentation/datagrid/connecting-to-adaptors/url-adaptor

**Online example**: https://blazor.syncfusion.com/demos/datagrid/remote-data?theme=bootstrap5