// Learn more about F# at http://fsharp.org

open System
open RealLifeTest.Types
open Thoth.Json.Newtonsoft

[<EntryPoint>]
let main argv =
    let operationData = {
        CreateOperationData.Employee = EmployeeId (Guid.NewGuid())
        Gender = Male
        Name = ComplexName {
            FirstName = "Homer"
            MiddleInitial = Some "J"
            LastName = "Simpson"
        }
        DistanceToOffice = Distance 9.56
        Children = [
            SimpleName {
                FirstName = "Bart"
                LastName = "Simpson"
            }
            ComplexName {
                FirstName = "Lisa"
                MiddleInitial = None
                LastName = "Simpson"
            }
            FullName "Maggie Simpson"
        ]
        WorkRelationship = WorkRelationship.OpenEnd (OpenEnd.OpenEnd "1990-09-01")
    }

    let json = Encode.Auto.toString(4, operationData, caseStrategy=CaseStrategy.CamelCase)
    let deserialized = Decode.Auto.unsafeFromString(json, caseStrategy=CaseStrategy.CamelCase)

    let result = operationData = deserialized

    printfn "%s %b" json result
    0 // return an integer exit code

