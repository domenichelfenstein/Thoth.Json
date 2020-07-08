module RealLifeTest.Types

    open System

    type EmployeeId = EmployeeId of Guid
    type Gender =
        | Female
        | Male
        | Other

    type SimpleName = {
        FirstName: string
        LastName: string
    }
    type ComplexName = {
        FirstName: string
        MiddleInitial: string option
        LastName: string
    }
    type Name =
        | FullName of string
        | SimpleName of SimpleName
        | ComplexName of ComplexName

    [<Measure>]
    type km

    type Distance = Distance of double

    type OpenEnd = OpenEnd of string

    type WorkRelationship =
        | OpenEnd of OpenEnd
        | Terminated of string * string

    type CreateOperationData = {
        Employee: EmployeeId
        Gender: Gender
        Name: Name
        DistanceToOffice: Distance
        Children: Name list
        WorkRelationship: WorkRelationship
    }

