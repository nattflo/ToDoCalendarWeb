import { PeriodEditor } from "./PeriodEditor/PeriodEditor";

const Home = () => {
    
    return (
        <div>
            {/* <TaskEditor
                mode={TaskWrapperModes.Editing}
                periodId='06e6d9cb-8e1e-4640-b4c9-2acd4048e86d'
            /> */}
            <PeriodEditor
                routineId='06e6d9cb-8e1e-4640-b4c9-2acd4048e86d'
            />
        </div>
    );

}

export { Home };
