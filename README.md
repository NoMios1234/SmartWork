# SmartWork

The software system of flexible adjustment of the work process in coworking aims to provide a comfortable search for the coworking area, to provide as much information as possible about the workspace that the client is considering. SmartWork provides for the organization of transparent access to the review of technical and material support of the coworking area, available rooms, indicators of their temperature and lighting


Packages:


    <PackageReference Include="Microsoft.AspNet.WebApi" Version="5.2.7" />
    <PackageReference Include="Microsoft.AspNet.WebApi.Client" Version="5.2.7" />
    <PackageReference Include="Microsoft.AspNet.WebApi.Core" Version="5.2.7" />
    <PackageReference Include="Microsoft.AspNet.WebApi.Cors" Version="5.2.7" />
    <PackageReference Include="Microsoft.AspNet.WebApi.HelpPage" Version="5.2.7" />
    <PackageReference Include="Microsoft.AspNet.WebApi.Owin" Version="5.2.7" />
    <PackageReference Include="Microsoft.AspNet.WebApi.WebHost" Version="5.2.7" />
    <PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="5.0.4" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc.NewtonsoftJson" Version="5.0.5" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="5.0.4" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="5.0.4">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.TypeScript.MSBuild" Version="4.2.4">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Newtonsoft.Json" Version="12.0.3" />
    <PackageReference Include="QRCoder" Version="1.4.1" />
    
    
    
Add Cors:


  in ConfigureServices:
  
  
    services.AddCors(c => {
                    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
                    .AllowAnyHeader());
                });
  in Configure:
  
  
    app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
