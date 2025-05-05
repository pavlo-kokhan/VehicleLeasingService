'use client';

import { useState, useEffect } from "react";

import VehiclesTableSettings from "@/components/vehicles/VehiclesTableSettings";
import VehiclesTable from "@/components/vehicles/VehiclesTable";
import VehicleAddOrEdit from "@/components/vehicles/VehicleAddOrEdit";
import {getVehicles} from "@/api/vehicles";

export default function Vehicles() {
  // const [isOpen, setIsOpen] = useState(false);
  // const [isAddForm, setIsAddForm] = useState(true);
  // const [selectedVehicle, setSelectedVehicle] = useState(null);
  // const [vehicless, setVehicles] = useState(null);

  return (
    <div className="row">
      this shit
      {/*<div className="col-2">*/}
      {/*    <VehiclesTableSettings setVehicles={setVehicles} />*/}
      {/*</div>*/}
      {/*<div className="col-10">*/}
      {/*    <div className="d-flex justify-content-end mb-3">*/}
      {/*        <button*/}
      {/*            onClick={()=> {*/}
      {/*                setIsOpen(true);*/}
      {/*                setIsAddForm(true);*/}
      {/*            }}*/}
      {/*        >*/}
      {/*            add new vehicle*/}
      {/*        </button>*/}
      {/*    </div>*/}
      {/*    <VehiclesTable*/}
      {/*        vehicless={vehicless}*/}
      {/*        setVehicles={setVehicles}*/}
      {/*        setIsOpen={setIsOpen}*/}
      {/*        setIsAddForm={setIsAddForm}*/}
      {/*        setSelectedVehicle={setSelectedVehicle}*/}
      {/*    />*/}
      {/*</div>*/}
      {/*{isOpen && (*/}
      {/*    <VehicleAddOrEdit*/}
      {/*        setVehicles={setVehicles}*/}
      {/*        setIsOpen={setIsOpen}*/}
      {/*        isAddForm={isAddForm}*/}
      {/*        vehicle={selectedVehicle}*/}
      {/*    />*/}
      {/*)*/}
      {/*}*/}
    </div>
  );
}
