# the minimum version this module was tested against
cmake_minimum_required(VERSION 3.8.0 FATAL_ERROR)

if (WIN32)


# include the c# utilities
include(${CMAKE_ROOT}/Modules/CSharpUtilities.cmake)


# obtain all the external libraries

set(BOARD_CLASSES
   Board.cs
   BoardRow.cs
   BoardLocation.cs
)

set(SOURCE_FILES
   BasicObject.cs
   FileUtilities.cs
   GameConfiguration.cs
   GameManager.cs
   HeaderSquare.cs
   Money.cs
   Program.cs
   Properties/AssemblyInfo.cs
   Square.cs
   SquareGenerator.cs
   Score.cs
   Teams.cs
   User.cs
   UserList.cs
   WinnerClass.cs
   Properties/Settings.settings
   App.config
)

set(FORM_FILES
   Properties/Resources.Designer.cs
   Properties/Resources.resx
   Properties/Settings.Designer.cs
   SquaresBoardForm.Designer.cs
   SquaresBoardForm.cs
   SquaresBoardForm.resx
   TeamNameForm.Designer.cs
   TeamNameForm.cs
   TeamNameForm.resx
   UserForm.Designer.cs
   UserForm.cs
   UserForm.resx
)

# define all the source files for the build
set(source
   ${BOARD_CLASSES}
   ${FORM_FILES}
   ${SOURCE_FILES}
)

csharp_set_windows_forms_properties(${FORM_FILES})

# define the library name and the project type
set(proj_name Party_Squares_Game)
add_executable(${proj_name} WIN32 ${source})
set_target_properties(${proj_name} PROPERTIES DEBUG_POSTFIX "D")
set(CMAKE_CSharp_FLAGS "/langversion:7")
set_target_properties(${proj_name} PROPERTIES VS_DOTNET_TARGET_FRAMEWORK_VERSION "v4.6")

# set the default namespace for the project
set_target_properties(${proj_name} PROPERTIES VS_GLOBAL_ROOTNAMESPACE ${proj_name})


# set the system references needed for the project
set_target_properties(${proj_name}
                      PROPERTIES
                      VS_DOTNET_REFERENCES
                      "System;System.Core;System.Xml.Linq;System.Data.DataSetExtensions;Microsoft.CSharp;System.Data;System.Deployment;System.Drawing;System.Net.Http;System.Windows.Forms;System.Xml"
                      )

# Set the output directory
set_target_properties(${proj_name}
                      PROPERTIES
                      ARCHIVE_OUTPUT_DIRECTORY "${CMAKE_SOURCE_DIR}/Target/lib"
                      LIBRARY_OUTPUT_DIRECTORY "${CMAKE_SOURCE_DIR}/Target/lib"
                      RUNTIME_OUTPUT_DIRECTORY "${CMAKE_SOURCE_DIR}/Target/bin"
)

# define all compiler defines for this project and user of this project

# define all the includes for this project and users of this project

# define all the link libraries for this project and users of this project
target_link_libraries(${proj_name} PRIVATE JSONParser)

# add dependencies of this project on other projects

endif ()