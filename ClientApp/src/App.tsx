import React from 'react';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { RoutineEditor } from './components/RoutineEditor/RoutineEditor';

import './custom.css'
import {  Route, Routes } from 'react-router-dom';

const App = () => {
  
    return (
      <Layout>
        <Routes>
          <Route path='/' element={<Home/>}/>
          <Route path='/routine/:id' element={<RoutineEditor/>}/>
        </Routes>
      </Layout>
    );
}

export default App;
