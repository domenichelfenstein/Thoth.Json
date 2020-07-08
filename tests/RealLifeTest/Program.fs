// Learn more about F# at http://fsharp.org

open System
open Newtonsoft.Json.Linq
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
        WorkRelationship = WorkRelationship.Terminated ("1990-01-01", "2010-04-05")
    }

    let tryParseGuid (guidString: string) =
        let parseResult, guid = Guid.TryParse guidString
        if parseResult then
            Some guid
        else
            None

    let isGuid (json: JsonValue) =
        not(isNull json) && json.Type = JTokenType.String

    let extra =
        Extra.empty
        |> Extra.withCustom
               (fun guid -> JValue(guid.ToString().ToUpper()) :> JsonValue)
               (fun json ->
                    if isGuid json then
                        let guidString = json.Value<string>()
                        match guidString |> tryParseGuid with
                        | Some guid -> Ok guid
                        | None ->  Error <| (guidString, FailMessage("content is no guid"))
                    else
                        Error <| (json.ToString(), FailMessage("content is no string")))

    let json = Encode.Auto.toString(4, operationData, caseStrategy=CaseStrategy.CamelCase, extra=extra)
    let deserialized = Decode.Auto.unsafeFromString(json, caseStrategy=CaseStrategy.CamelCase, extra=extra)

    let result = operationData = deserialized

    printfn "%s %b" json result
    0 // return an integer exit code

